using System;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;
using UnityEngine.UIElements;

public class UIHook : MonoBehaviour
{
    public RenderTexture[] PlayerPreviews = new RenderTexture[4];
    public VisualTreeAsset PlayerInputDescription;
    public ControllerNameAndTextureCollectionPair[] ControllerNameAndTexturePairs = Array.Empty<ControllerNameAndTextureCollectionPair>();
    
    [Serializable]
    public struct ControllerNameAndTextureCollectionPair
    {
        public string Name;
        public ControllerSchemeTextureCollection TextureCollection;
    }
    
    public ControllerSchemeTextureCollection GetSchemeTextureCollectionFromName(string name) 
        => ControllerNameAndTexturePairs.FirstOrDefault(pair => pair.Name == name).TextureCollection;

    public static UIHook Instance { get; private set; }
    public static UIDocument UIDocument { get; private set; }
    void Awake()
    {
        // Cache the UIDocument
        UIDocument = GetComponent<UIDocument>();
        Instance = this;
        
        // Create a UIHookReady entity to signal that the UI is ready
        World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(UIHookReady));
    }
}

struct IsInGame : IComponentData { }
struct UIHookReady : IComponentData { }
struct InputPuppetTag : IComponentData, IEnableableComponent { }
class ControllerReference : IComponentData
{
    public KyleInput Value;
}

class InputPuppetReferences : IComponentData
{
    public InputDevice Device;
    
    public VisualElement RootVisualElement;
    public VisualElement Preview;
    public Label Name;
    public VisualElement Top;
}
struct OriginalOwnerSlotIndexAndUser : IComponentData
{
    public int SlotIndex;
    public InputUser User;
}

partial struct UISystem : ISystem, ISystemStartStop
{
    public class SingletonManaged : IComponentData
    {
        public VisualElement RootVisualElement;
        public Button StartButton;
        public Button ExitButton;
        public VisualElement EnterGameMenu;
        public VisualElement MainMenu;
        public VisualElement GameHideNotUsedPlayer4Region;
    }
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<UIHookReady>();
        state.EntityManager.AddComponentData(state.SystemHandle, new SingletonManaged());
    }
    
    public void OnStartRunning(ref SystemState state)
    {
        var singletonManaged = state.EntityManager.GetComponentData<SingletonManaged>(state.SystemHandle);
        singletonManaged.RootVisualElement = UIHook.UIDocument.rootVisualElement;
        singletonManaged.StartButton = singletonManaged.RootVisualElement.Q<Button>("Start");
        singletonManaged.ExitButton = singletonManaged.RootVisualElement.Q<Button>("Exit");
        singletonManaged.EnterGameMenu = singletonManaged.RootVisualElement.Q<VisualElement>("EnterGameMenu");
        singletonManaged.MainMenu = singletonManaged.RootVisualElement.Q<VisualElement>("WrappedOuter");
        singletonManaged.GameHideNotUsedPlayer4Region = singletonManaged.RootVisualElement.Q<VisualElement>("GameHideNotUsedPlayer4Region");
        var em = state.EntityManager;
        
        singletonManaged.StartButton.clicked += () =>
        {
            singletonManaged.MainMenu.style.display = DisplayStyle.None;
            em.CreateEntity(stackalloc []{ ComponentType.ReadWrite<IsInEnterGameMenu>() });
        };
        
        singletonManaged.ExitButton.clicked += () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        };
    }

    public void OnStopRunning(ref SystemState state) {}
}

struct IsInEnterGameMenu : IComponentData { }
partial struct EnterGameMenuSystem : ISystem, ISystemStartStop
{
    public struct MenuState : IComponentData
    {
        public bool4 SlotOccupied;
        public bool4 PlayerReady;
        
        public int ClaimFirstAvailableSlot()
        {
            for (var i = 0; i < 4; i++)
            {
                if (!SlotOccupied[i])
                {
                    SlotOccupied[i] = true;
                    return i;
                }
            }
            return -1;
        }
        
        public readonly (int playersReady, int totalRequiredPlayers) GetPlayerJoinStatus()
        {
            var playersReady = math.csum((int4)PlayerReady);
            var totalRequiredPlayers = math.max(2, InputUser.all.Count);
            return (playersReady, totalRequiredPlayers);
        }
        
        public readonly string GetWaitingPlayersText()
        {
            var (playersReady, totalRequiredPlayers) = GetPlayerJoinStatus();
            return playersReady == totalRequiredPlayers 
                ? $"All players ready ({playersReady}/{totalRequiredPlayers})" 
                : $"Waiting for players ({playersReady}/{totalRequiredPlayers})";
        }
    }

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        state.RequireForUpdate<IsInEnterGameMenu>();
        state.EntityManager.AddComponentData(state.SystemHandle, new MenuState());
    }

    public void OnStartRunning(ref SystemState state)
    {
        var em = state.EntityManager;
        var systemHandle = state.SystemHandle;

        var uiSystem = SystemAPI.ManagedAPI.GetSingleton<UISystem.SingletonManaged>();
        
        // Show the player selection menu and set it up
        uiSystem.EnterGameMenu.style.display = DisplayStyle.Flex;
        var waitText = uiSystem.EnterGameMenu.Q<Label>("WaitText");
        waitText.text = "Waiting for players (0/2)";
        
        // Listen for up to 4 controllers to be paired
        InputUser.listenForUnpairedDeviceActivity = 4;
        InputUser.onUnpairedDeviceUsed += (control, _) =>
        {
            if (!(control is ButtonControl)) 
                return;
            if (control.device.description.deviceClass == "Mouse") 
                return;
            
            // Find the first available slot
            var menuState = em.GetComponentDataRW<MenuState>(systemHandle);
            var availableSlot = menuState.ValueRW.ClaimFirstAvailableSlot();
            if (availableSlot == -1)
                return;
            
            // Pair the device with the user, making the playerController only act with the given device
            var user = InputUser.PerformPairingWithDevice(control.device);
            var playerController = new KyleInput();
            playerController.Enable();
            user.AssociateActionsWithUser(playerController);
            
            // if keyboard also pair with mouse
            if (control.device.description.deviceClass == "Keyboard")
            {
                var mouse = InputSystem.devices.FirstOrDefault(d => d.description.deviceClass == "Mouse");
                if (mouse != null)
                    InputUser.PerformPairingWithDevice(mouse, user);
            }
            
            // Create the InputPuppet entity (stores the controller and device to be queried later)
            var inputPuppetEntity = em.CreateEntity(stackalloc []{ ComponentType.ReadWrite<InputPuppetTag>(), ComponentType.ReadWrite<OriginalOwnerSlotIndexAndUser>() });
            em.SetName(inputPuppetEntity, $"InputPuppet for S{availableSlot + 1}");
            em.AddComponentObject(inputPuppetEntity, new ControllerReference { Value = playerController});
            em.AddComponentObject(inputPuppetEntity, new InputPuppetReferences
            {
                Device = control.device,
            });
            em.SetComponentData(inputPuppetEntity, new OriginalOwnerSlotIndexAndUser
            {
                SlotIndex = availableSlot,
                User = user
            });
            
            InputUser.listenForUnpairedDeviceActivity--;
        };
    }

    public void OnStopRunning(ref SystemState state) {}

    public void OnUpdate(ref SystemState state)
    {
        var uiSystem = SystemAPI.ManagedAPI.GetSingleton<UISystem.SingletonManaged>();
        var waitText = uiSystem.EnterGameMenu.Q<Label>("WaitText");
        
        // For each active player, set up the UI (InputPuppetTag is used to determine if the player has been set up)
        foreach (var (puppetStillNeedsInit, slotAndUserIndex, controllerReference, puppetReferences) 
                 in SystemAPI.Query<EnabledRefRW<InputPuppetTag>, RefRO<OriginalOwnerSlotIndexAndUser>, ControllerReference, InputPuppetReferences>())
        {
            var playerController = controllerReference.Value;
            var device = puppetReferences.Device;
            var availableSlot = slotAndUserIndex.ValueRO.SlotIndex;
            
            // Set up the player preview images
            puppetReferences.RootVisualElement = uiSystem.EnterGameMenu.Q<VisualElement>($"Player{availableSlot + 1}Pane");
            puppetReferences.Preview = puppetReferences.RootVisualElement.Q<VisualElement>("preview");
            puppetReferences.Top = puppetReferences.RootVisualElement.Q<VisualElement>("top");
            puppetReferences.Name = puppetReferences.RootVisualElement.Q<Label>("Name");
            
            puppetReferences.Preview.style.backgroundImage = Background.FromRenderTexture(UIHook.Instance.PlayerPreviews[availableSlot]);
            puppetReferences.Name.text = "Not Ready (press to ready)";
            
            // List Controls in the UI
            var texturesForController = UIHook.Instance.GetSchemeTextureCollectionFromName($"{device.description.interfaceName}:{device.description.product}");

            foreach (var action in playerController)
            {
                if (action.actionMap == (InputActionMap)playerController.GameMenu)
                    continue;
                
                var actionElement = UIHook.Instance.PlayerInputDescription.CloneTree();
                actionElement.Q<Label>("ActionName").text = action.name;
                actionElement.Q<VisualElement>("Icon").style.backgroundImage = texturesForController.GetFromActionAndDevice(action, device);
                puppetReferences.Top.Add(actionElement);
            }
            puppetReferences.RootVisualElement.Q<VisualElement>("ReadyButton").style.backgroundImage = texturesForController.GetFromActionAndDevice(playerController.GameMenu.Ready, device);
            
            waitText.text = SystemAPI.GetComponent<MenuState>(state.SystemHandle).GetWaitingPlayersText();
            
            puppetStillNeedsInit.ValueRW = false;
        }
        
        
        var menuState = SystemAPI.GetComponentRW<MenuState>(state.SystemHandle);
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);
        
        // Check if the player is ready or not, and if all players are ready, allow them to enter the game
        foreach (var (slotAndUserIndex, controllerReference, puppetReferences, inputPuppetEntity) 
                 in SystemAPI.Query<RefRO<OriginalOwnerSlotIndexAndUser>, ControllerReference, InputPuppetReferences>().WithEntityAccess())
        {
            var playerController = controllerReference.Value;
            var availableSlot = slotAndUserIndex.ValueRO.SlotIndex;
            
            if (playerController.GameMenu.Ready.triggered)
            {
                menuState.ValueRW.PlayerReady[availableSlot] = !menuState.ValueRO.PlayerReady[availableSlot];
                puppetReferences.Name.text = menuState.ValueRO.PlayerReady[availableSlot] ? "Ready" : "Not Ready (press to ready)";
                waitText.text = menuState.ValueRO.GetWaitingPlayersText();
            };
            
            if (playerController.GameMenu.EnterGame.triggered)
            {
                var (playersReady, totalRequiredPlayers) = menuState.ValueRO.GetPlayerJoinStatus();
                if (playersReady < 2 || playersReady < totalRequiredPlayers)
                    return;
                
                uiSystem.EnterGameMenu.style.display = DisplayStyle.None;
                state.EntityManager.CreateEntity(stackalloc []{ ComponentType.ReadWrite<IsInGame>() });
            }
            
            // Pressing escape in the menu will unpair the device
            if (playerController.GameMenu.Cancel.triggered)
            {
                ecb.DestroyEntity(inputPuppetEntity);
                puppetReferences.Preview.style.backgroundImage = null;
                puppetReferences.Name.text = "Any button to connect";
                while (puppetReferences.Top.childCount > 1) 
                    puppetReferences.Top.RemoveAt(1);
                menuState.ValueRW.SlotOccupied[availableSlot] = false;
                menuState.ValueRW.PlayerReady[availableSlot] = false;
                slotAndUserIndex.ValueRO.User.UnpairDevicesAndRemoveUser();
                InputUser.listenForUnpairedDeviceActivity++;
                waitText.text = menuState.ValueRO.GetWaitingPlayersText();
            };
        }
    }
}


[UpdateInGroup(typeof(InitializationSystemGroup))]
partial struct GameStartSystem : ISystem, ISystemStartStop
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<IsInGame>();
        state.RequireForUpdate<EnterGameMenuSystem.MenuState>();
    }

    public void OnStartRunning(ref SystemState state)
    {
        // Disable all cameras that are tagged with "DisableInGame"
        foreach (var cam in Camera.allCameras)
        {
            if (cam.CompareTag("DisableInGame"))
                cam.gameObject.SetActive(false);
        }
        
        var playersReady = SystemAPI.GetSingleton<EnterGameMenuSystem.MenuState>().GetPlayerJoinStatus().playersReady;
        var uiSystem = SystemAPI.ManagedAPI.GetSingleton<UISystem.SingletonManaged>();
        var spawnOnPlayerSpawn =
            SystemAPI.GetSingletonBuffer<SpawnOnPlayerSpawn>().AsNativeArray().Reinterpret<Entity>();
        
        // Set up the cameras for the players and spawn the characters
        var playerIndex = 0;
        foreach (var (slotAndUserIndex, controllerReference) in SystemAPI.Query<OriginalOwnerSlotIndexAndUser, ControllerReference>())
        {
            // Disable player's EnterGameMenu input and enable the camera
            controllerReference.Value.GameMenu.Disable();
            var camForPlayer = SyncWithPlayerOrbitCamera.Instance[slotAndUserIndex.User.index];
            camForPlayer.gameObject.SetActive(true);

            // Set the camera to the correct position (2 players split top and bottom, 3 players split top left and right, and bottom left, default is 4 players split into 4 regions)
            switch (playersReady)
            {
                case 2:
                    // Split the screen in half top and bottom
                    camForPlayer.rect = playerIndex == 0 ? new Rect(0, 0.5f, 1, 0.5f) : new Rect(0, 0, 1, 0.5f);
                    break;
                case 3 when playerIndex == 0:
                    uiSystem.GameHideNotUsedPlayer4Region.style.display = DisplayStyle.Flex;
                    break;
            }

            // Spawn the character
            RefRW<ThirdPersonCharacterData> charDataRef = default;
            foreach (var entity in spawnOnPlayerSpawn)
            {
                var spawned = state.EntityManager.Instantiate(entity);
                if (SystemAPI.HasComponent<ThirdPersonCharacterData>(spawned))
                {
                    charDataRef = SystemAPI.GetComponentRW<ThirdPersonCharacterData>(spawned);
                    
                    // Set the starting position of the character
                    var pos = new float3(slotAndUserIndex.User.index* 2, 0, 0);
                    SystemAPI.SetComponent(spawned, LocalTransform.FromPosition(pos));
                    SystemAPI.GetComponentRW<AnimatorInstantiationData>(charDataRef.ValueRO.AnimationEntity).ValueRW.LookIndex = slotAndUserIndex.SlotIndex;
                    SystemAPI.ManagedAPI.GetComponent<ControllerReference>(spawned).Value = controllerReference.Value;
                }
                if (SystemAPI.HasComponent<OrbitCamera>(spawned))
                {
                    Debug.Assert(charDataRef.IsValid, $"{nameof(ThirdPersonCharacterData)} must be spawned before {nameof(OrbitCamera)}");;
                    SystemAPI.GetComponentRW<PlayerID>(spawned).ValueRW.Value = (byte)slotAndUserIndex.User.index;
                    charDataRef.ValueRW.ControlledCamera = spawned;
                }
            }
            
            playerIndex++;
        }
    }

    public void OnStopRunning(ref SystemState state) {}
}
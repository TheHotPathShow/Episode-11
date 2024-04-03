using System;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
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

struct QueueToStartGame : IComponentData { }
struct UIHookReady : IComponentData { }
struct InputPuppetTag : IComponentData { }
class ControllerReference : IComponentData
{
    public KyleInput Value;
}
struct OriginalOwnerSlotAndUserIndex : IComponentData
{
    public int SlotIndex;
    public int UserIndex;
}

partial struct UISystem : ISystem, ISystemStartStop
{
    class SingletonManaged : IComponentData
    {
        public bool4 slotOccupied;
        public bool4 playerReady;
        
        public int ClaimFirstAvailableSlot()
        {
            for (var i = 0; i < 4; i++)
            {
                if (!slotOccupied[i])
                {
                    slotOccupied[i] = true;
                    return i;
                }
            }
            return -1;
        }
        
        public (int playersReady, int totalRequiredPlayers) GetPlayerJoinStatus()
        {
            var playersReady = math.csum((int4)playerReady);
            var totalRequiredPlayers = math.max(2, InputUser.all.Count);
            return (playersReady, totalRequiredPlayers);
        }
        
        public string GetWaitingPlayersText()
        {
            var (playersReady, totalRequiredPlayers) = GetPlayerJoinStatus();
            return playersReady == totalRequiredPlayers 
                ? $"All players ready ({playersReady}/{totalRequiredPlayers})" 
                : $"Waiting for players ({playersReady}/{totalRequiredPlayers})";
        }
    }
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<UIHookReady>();
        state.EntityManager.AddComponentData(state.SystemHandle, new SingletonManaged());
    }
    
    public void OnStartRunning(ref SystemState state)
    {
        var singletonManaged = state.EntityManager.GetComponentData<SingletonManaged>(state.SystemHandle);
        var em = state.EntityManager;
        
        UIHook.UIDocument.rootVisualElement.Q<Button>("Start").clicked += () =>
        {
            // Hide the main menu and show the player selection menu
            UIHook.UIDocument.rootVisualElement.Q<VisualElement>("WrappedOuter").style.display = DisplayStyle.None;
            var enterGameMenu = UIHook.UIDocument.rootVisualElement.Q<VisualElement>("EnterGameMenu");
            enterGameMenu.style.display = DisplayStyle.Flex;
            
            // Set up the player selection menu
            var waitText = enterGameMenu.Q<Label>("WaitText");
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
                var availableSlot = singletonManaged.ClaimFirstAvailableSlot();
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
                
                // Set up the player preview images
                var playerPane = UIHook.UIDocument.rootVisualElement.Q<VisualElement>($"Player{availableSlot + 1}Pane");
                playerPane.Q<VisualElement>("preview").style.backgroundImage = Background.FromRenderTexture(UIHook.Instance.PlayerPreviews[availableSlot]);
                playerPane.Q<Label>("Name").text = $"Not Ready (press to ready)";
                
                // List Controls in the UI
                var texturesForController = UIHook.Instance.GetSchemeTextureCollectionFromName($"{control.device.description.interfaceName}:{control.device.description.product}");
                var top = playerPane.Q<VisualElement>("top");
                foreach (var action in playerController)
                {
                    if (action.actionMap == (InputActionMap)playerController.GameMenu)
                        continue;
                    
                    var actionElement = UIHook.Instance.PlayerInputDescription.CloneTree();
                    actionElement.Q<Label>("ActionName").text = action.name;
                    actionElement.Q<VisualElement>("Icon").style.backgroundImage = texturesForController.GetFromActionAndDevice(action, control.device);
                    top.Add(actionElement);
                }
                playerPane.Q<VisualElement>("ReadyButton").style.backgroundImage = texturesForController.GetFromActionAndDevice(playerController.Player.Jump, control.device);

                var inputPuppetEntity = em.CreateEntity(stackalloc []{ ComponentType.ReadWrite<InputPuppetTag>(), ComponentType.ReadWrite<OriginalOwnerSlotAndUserIndex>() });
                em.SetName(inputPuppetEntity, $"InputPuppet for S{availableSlot + 1}");
                em.AddComponentObject(inputPuppetEntity, new ControllerReference { Value = playerController });
                em.SetComponentData(inputPuppetEntity, new OriginalOwnerSlotAndUserIndex
                {
                    SlotIndex = availableSlot,
                    UserIndex = user.index
                });
                
                waitText.text = singletonManaged.GetWaitingPlayersText();
                playerController.GameMenu.Ready.started += _ =>
                {
                    singletonManaged.playerReady[availableSlot] = !singletonManaged.playerReady[availableSlot];
                    playerPane.Q<Label>("Name").text = singletonManaged.playerReady[availableSlot] ? "Ready" : "Not Ready (press to ready)";
                    waitText.text = singletonManaged.GetWaitingPlayersText();
                };
                
                playerController.GameMenu.EnterGame.started += _ =>
                {
                    var (playersReady, totalRequiredPlayers) = singletonManaged.GetPlayerJoinStatus();
                    if (playersReady < 2 || playersReady < totalRequiredPlayers)
                        return;
                    
                    foreach (var cam in Camera.allCameras)
                    {
                        if (cam.CompareTag("DisableInGame"))
                            cam.gameObject.SetActive(false);
                    }
                    
                    using var puppets = new EntityQueryBuilder(Allocator.Temp).WithAll<OriginalOwnerSlotAndUserIndex>().Build(em);
                    using var originalOwners = puppets.ToComponentDataArray<OriginalOwnerSlotAndUserIndex>(Allocator.Temp);
                    var inputPuppets = puppets.ToComponentArray<ControllerReference>();
                    for (var i = 0; i < playersReady; i++)
                    {
                        inputPuppets[i].Value.GameMenu.Disable();
                        var camForPlayer = SyncWithPlayerOrbitCamera.Instance[originalOwners[i].UserIndex];
                        camForPlayer.gameObject.SetActive(true);

                        switch (playersReady)
                        {
                            case 2:
                                // Split the screen in half top and bottom
                                camForPlayer.rect = i == 0 ? new Rect(0, 0.5f, 1, 0.5f) : new Rect(0, 0, 1, 0.5f);
                                break;
                            case 3:
                                UIHook.UIDocument.rootVisualElement.Q<VisualElement>("GameHideNotUsedPlayer4Region").style.display = DisplayStyle.Flex;
                                break;
                        }
                    }
                    
                    enterGameMenu.style.display = DisplayStyle.None;
                    em.CreateEntity(stackalloc []{ ComponentType.ReadWrite<QueueToStartGame>() });
                };
                
                // Pressing escape in the menu will unpair the device
                playerController.GameMenu.Cancel.started += _ =>
                {
                    em.DestroyEntity(inputPuppetEntity);
                    playerPane.Q<VisualElement>("preview").style.backgroundImage = null;
                    playerPane.Q<Label>("Name").text = "Any button to connect";
                    while (top.childCount > 1) 
                        top.RemoveAt(1);
                    singletonManaged.slotOccupied[availableSlot] = false;
                    singletonManaged.playerReady[availableSlot] = false;
                    user.UnpairDevicesAndRemoveUser();
                    InputUser.listenForUnpairedDeviceActivity++;
                    waitText.text = singletonManaged.GetWaitingPlayersText();
                };
                
                InputUser.listenForUnpairedDeviceActivity--;
            };
        };
        
        UIHook.UIDocument.rootVisualElement.Q<Button>("Exit").clicked += () =>
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


[UpdateInGroup(typeof(InitializationSystemGroup))]
partial struct GameStartSystem : ISystem, ISystemStartStop
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<QueueToStartGame>();
    }

    public void OnStartRunning(ref SystemState state)
    {
        var spawnOnPlayerSpawn =
            SystemAPI.GetSingletonBuffer<SpawnOnPlayerSpawn>().AsNativeArray().Reinterpret<Entity>();
        
        foreach (var (controllerReference, slotAndUserIndex) in SystemAPI.Query<ControllerReference, OriginalOwnerSlotAndUserIndex>().WithAll<InputPuppetTag>())
        {
            RefRW<ThirdPersonCharacterData> charDataRef = default;
            foreach (var entity in spawnOnPlayerSpawn)
            {
                var spawned = state.EntityManager.Instantiate(entity);
                if (SystemAPI.HasComponent<ThirdPersonCharacterData>(spawned))
                {
                    charDataRef = SystemAPI.GetComponentRW<ThirdPersonCharacterData>(spawned);
                    
                    // Set the starting position of the character
                    var pos = new float3(slotAndUserIndex.UserIndex* 2, 0, 0);
                    SystemAPI.SetComponent(spawned, LocalTransform.FromPosition(pos));
                    SystemAPI.GetComponentRW<AnimatorInstantiationData>(charDataRef.ValueRO.AnimationEntity).ValueRW.LookIndex = slotAndUserIndex.SlotIndex;
                    SystemAPI.ManagedAPI.GetComponent<ControllerReference>(spawned).Value = controllerReference.Value;
                }
                if (SystemAPI.HasComponent<OrbitCamera>(spawned))
                {
                    Debug.Assert(charDataRef.IsValid, $"{nameof(ThirdPersonCharacterData)} must be spawned before {nameof(OrbitCamera)}");;
                    SystemAPI.GetComponentRW<PlayerID>(spawned).ValueRW.Value = (byte)slotAndUserIndex.UserIndex;
                    charDataRef.ValueRW.ControlledCamera = spawned;
                }
            }
        }
    }

    public void OnStopRunning(ref SystemState state) {}
}
using System;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

public class UIHook : MonoBehaviour
{
    public RenderTexture[] PlayerPreviews = new RenderTexture[4];
    
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

struct UIHookReady : IComponentData { }
struct InputPuppetTag : IComponentData { }
class ControllerReference : IComponentData
{
    public KyleInput Value;
}

partial struct UISystem : ISystem, ISystemStartStop
{
    class SingletonManaged : IComponentData
    {
        public KyleInput globalInput;
        public bool4 slotOccupied;
    }
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<UIHookReady>();
        state.EntityManager.AddComponentData(state.SystemHandle, new SingletonManaged());
    }
    
    public void OnStartRunning(ref SystemState state)
    {
        var singletonManaged = state.EntityManager.GetComponentData<SingletonManaged>(state.SystemHandle);
        singletonManaged.globalInput = new KyleInput();
        singletonManaged.globalInput.Enable();
        singletonManaged.globalInput.Player.Start.started += ctx =>
        {
            Debug.Log($"Player number {InputUser.FindUserPairedToDevice(ctx.control.device)!.Value.index} pressed start");
        };
        
        var em = state.EntityManager;
        
        UIHook.UIDocument.rootVisualElement.Q<Button>("Start").clicked += () =>
        {
            // Hide the main menu and show the player selection menu
            UIHook.UIDocument.rootVisualElement.Q<VisualElement>("WrappedOuter").style.display = DisplayStyle.None;
            UIHook.UIDocument.rootVisualElement.Q<VisualElement>("Enter Game Menu").style.display = DisplayStyle.Flex;
            
            // Listen for up to 4 controllers to be paired
            InputUser.listenForUnpairedDeviceActivity = 4;
            InputUser.onUnpairedDeviceUsed += UnpairedDeviceUsed;
            return;

            // Local function to handle unpaired devices
            void UnpairedDeviceUsed(InputControl control, InputEventPtr ptr)
            {
                if (!(control is ButtonControl)) 
                    return;
                if (control.device.description.deviceClass == "Mouse") 
                    return;
                
                // Find the first available slot
                var availableSlot = -1;
                for (var slotIndex = 0; slotIndex < 4; slotIndex++)
                {
                    if (singletonManaged.slotOccupied[slotIndex])
                        continue;
                    availableSlot = slotIndex;
                    singletonManaged.slotOccupied[slotIndex] = true;
                    break;
                }
                if (availableSlot == -1)
                    return;
                
                var user = InputUser.PerformPairingWithDevice(control.device);
                var playerController = new KyleInput();
                playerController.Enable();
                user.AssociateActionsWithUser(playerController);

                // List Controls in the UI
                // var texturesForController = new ControllerSchemeTextureCollection();
                // foreach (var action in playerController)
                // {
                //     var textureForAction = texturesForController.GetFromActionAndDevice(action, control.device);
                // }
                
                Debug.Log($"Created player controller {availableSlot + 1}");

                // Set up the player preview images
                var playerPane = UIHook.UIDocument.rootVisualElement.Q<VisualElement>($"Player{availableSlot + 1}Pane");
                playerPane.Q<VisualElement>("preview").style.backgroundImage = Background.FromRenderTexture(UIHook.Instance.PlayerPreviews[availableSlot]);
                playerPane.Q<Label>("Name").text = $"Not Ready (press to ready)";
                // playerPane.Q<VisualElement>("ReadyButton").style.backgroundImage = texturesForController.GetFromActionAndDevice(playerController.Player.Jump, control.device);

                var inputPuppetEntity = em.CreateEntity(stackalloc []{ ComponentType.ReadWrite<InputPuppetTag>() });
                em.SetName(inputPuppetEntity, $"InputPuppet for S{availableSlot + 1}");
                em.AddComponentObject(inputPuppetEntity, new ControllerReference { Value = playerController });
                
                playerController.Player.Escape.started += ctx =>
                {
                    em.DestroyEntity(inputPuppetEntity);
                    playerPane.Q<VisualElement>("preview").style.backgroundImage = null;
                    playerPane.Q<Label>("Name").text = "Any button to connect";
                    singletonManaged.slotOccupied[availableSlot] = false;
                    user.UnpairDevicesAndRemoveUser();
                    InputUser.listenForUnpairedDeviceActivity++;
                };
                InputUser.listenForUnpairedDeviceActivity--;
            }
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

    public void OnStopRunning(ref SystemState state)
    {
        
    }
}
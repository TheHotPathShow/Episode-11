//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/UnityTechnologies/StarterAssets/InputSystem/StarterAssets.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @KyleInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @KyleInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""StarterAssets"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f62a4b92-ef5e-4175-8f4c-c9075429d32c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6bc1aaf4-b110-4ff7-891e-5b9fe6f32c4d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""2690c379-f54d-45be-a724-414123833eb4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8c4abdf8-4099-493a-aa1a-129acec7c3df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""980e881e-182c-404c-8cbf-3d09fdb48fef"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""cccc964b-4820-4412-962f-a3b5090debac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""b87d7374-bbe2-4813-b621-ea53ffac9da1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""b7594ddb-26c9-4ba2-bd5a-901468929edc"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2063a8b5-6a45-43de-851b-65f3d46e7b58"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""64e4d037-32e1-4fb9-80e4-fc7330404dfe"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0fce8b11-5eab-4e4e-a741-b732e7b20873"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7bdda0d6-57a8-47c8-8238-8aecf3110e47"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""IJKL"",
                    ""id"": ""91ce78fa-226b-47f7-9bf9-9739b0b0d9ed"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4a66c3bd-1a65-48b8-ad73-e94388a895c5"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0ddf7c99-6ed0-4b96-9a5d-6824870c85e1"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0002768d-8e4a-4cb1-8bbd-87b1aca378e7"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f83a77e4-acd9-4884-9315-767105a58cb2"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e2f9aa65-db06-4c5b-a2e9-41bc8acb9517"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed66cbff-2900-4a62-8896-696503cfcd31"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false,invertY=false),ScaleVector2(x=0.05,y=0.05)"",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1d171b6-19d8-47a6-ba3a-71b6a8e7b3c0"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false,invertY=false),StickDeadzone,ScaleVector2"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bd55a0b-761e-4ae4-89ae-8ec127e08a29"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f973413-5e27-4239-acee-38c4a63feeba"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34838a4f-9e2a-4798-867e-6041f5f9c2e1"",
                    ""path"": ""<Keyboard>/slash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc65b89f-9bd3-43fb-92af-d0d87ba5faa4"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8fcd86e-dcfd-4f88-8e93-b638cdbf3320"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33e97f35-1e11-475a-b32d-a50407d533ef"",
                    ""path"": ""<Keyboard>/n"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12e9c9b2-4460-4c52-833e-fac556c2cca3"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d1e3f4e-1470-4e40-9378-843bf21d5908"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fab8259-bb57-453e-a2a5-04e2ab77a5e0"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""117b59ee-0084-420b-8040-aff83d497780"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2d22830-e8dd-4163-b9f6-2d2e210d6fef"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f480cc72-9210-4e57-bfe5-c22e4fabe88a"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""d48430c6-9751-4dc3-a360-405dca547408"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""34d5ea93-573e-4526-80f7-df27b3ccfdab"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3717c664-289c-4fbd-9e9b-37856cfdadd4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e167cf0c-847d-4d3b-9d20-01cb9ba10b8d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e11a1990-f32f-4d32-be3a-02ff0fd7eb63"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""GameMenu"",
            ""id"": ""62bca930-861f-4b0f-88e9-afda5b37a79d"",
            ""actions"": [
                {
                    ""name"": ""EnterGame"",
                    ""type"": ""Button"",
                    ""id"": ""0b0d410a-aeba-4cc9-adc2-19479234b5d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ready"",
                    ""type"": ""Button"",
                    ""id"": ""2f4663be-ce9e-495a-a101-5749e44cfa09"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""1267c00f-6b20-4afa-9b24-64cf8489c8ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f3c9efb3-a7ca-485d-9986-1a7e5c1979eb"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""EnterGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24a29aac-3eb3-4205-9af2-85dd4cd6dc65"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""EnterGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89371bdb-ded8-4591-837a-53fccf36ed63"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""EnterGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfe3c785-4cf9-4a63-b8cf-633399e476f6"",
                    ""path"": ""<Keyboard>/slash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Ready"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f23a987b-0585-4cdd-a6e3-c01584cc6258"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Ready"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e13864c-cd12-4c6f-b869-b59b4ec07459"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Ready"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c921d06-ab71-466f-a576-cc70a53029ed"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardSecondary"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39704df5-0b1c-4976-9095-7d6cbeb6a315"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65f92ada-21ae-4579-b223-f2ac3b42ad84"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Xbox Controller"",
            ""bindingGroup"": ""Xbox Controller"",
            ""devices"": []
        },
        {
            ""name"": ""PS4 Controller"",
            ""bindingGroup"": ""PS4 Controller"",
            ""devices"": []
        },
        {
            ""name"": ""KeyboardSecondary"",
            ""bindingGroup"": ""KeyboardSecondary"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Start = m_Player.FindAction("Start", throwIfNotFound: true);
        m_Player_Escape = m_Player.FindAction("Escape", throwIfNotFound: true);
        // GameMenu
        m_GameMenu = asset.FindActionMap("GameMenu", throwIfNotFound: true);
        m_GameMenu_EnterGame = m_GameMenu.FindAction("EnterGame", throwIfNotFound: true);
        m_GameMenu_Ready = m_GameMenu.FindAction("Ready", throwIfNotFound: true);
        m_GameMenu_Cancel = m_GameMenu.FindAction("Cancel", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Start;
    private readonly InputAction m_Player_Escape;
    public struct PlayerActions
    {
        private @KyleInput m_Wrapper;
        public PlayerActions(@KyleInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Start => m_Wrapper.m_Player_Start;
        public InputAction @Escape => m_Wrapper.m_Player_Escape;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Start.started += instance.OnStart;
            @Start.performed += instance.OnStart;
            @Start.canceled += instance.OnStart;
            @Escape.started += instance.OnEscape;
            @Escape.performed += instance.OnEscape;
            @Escape.canceled += instance.OnEscape;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Start.started -= instance.OnStart;
            @Start.performed -= instance.OnStart;
            @Start.canceled -= instance.OnStart;
            @Escape.started -= instance.OnEscape;
            @Escape.performed -= instance.OnEscape;
            @Escape.canceled -= instance.OnEscape;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // GameMenu
    private readonly InputActionMap m_GameMenu;
    private List<IGameMenuActions> m_GameMenuActionsCallbackInterfaces = new List<IGameMenuActions>();
    private readonly InputAction m_GameMenu_EnterGame;
    private readonly InputAction m_GameMenu_Ready;
    private readonly InputAction m_GameMenu_Cancel;
    public struct GameMenuActions
    {
        private @KyleInput m_Wrapper;
        public GameMenuActions(@KyleInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @EnterGame => m_Wrapper.m_GameMenu_EnterGame;
        public InputAction @Ready => m_Wrapper.m_GameMenu_Ready;
        public InputAction @Cancel => m_Wrapper.m_GameMenu_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_GameMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameMenuActions set) { return set.Get(); }
        public void AddCallbacks(IGameMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_GameMenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameMenuActionsCallbackInterfaces.Add(instance);
            @EnterGame.started += instance.OnEnterGame;
            @EnterGame.performed += instance.OnEnterGame;
            @EnterGame.canceled += instance.OnEnterGame;
            @Ready.started += instance.OnReady;
            @Ready.performed += instance.OnReady;
            @Ready.canceled += instance.OnReady;
            @Cancel.started += instance.OnCancel;
            @Cancel.performed += instance.OnCancel;
            @Cancel.canceled += instance.OnCancel;
        }

        private void UnregisterCallbacks(IGameMenuActions instance)
        {
            @EnterGame.started -= instance.OnEnterGame;
            @EnterGame.performed -= instance.OnEnterGame;
            @EnterGame.canceled -= instance.OnEnterGame;
            @Ready.started -= instance.OnReady;
            @Ready.performed -= instance.OnReady;
            @Ready.canceled -= instance.OnReady;
            @Cancel.started -= instance.OnCancel;
            @Cancel.performed -= instance.OnCancel;
            @Cancel.canceled -= instance.OnCancel;
        }

        public void RemoveCallbacks(IGameMenuActions instance)
        {
            if (m_Wrapper.m_GameMenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_GameMenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameMenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameMenuActions @GameMenu => new GameMenuActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_XboxControllerSchemeIndex = -1;
    public InputControlScheme XboxControllerScheme
    {
        get
        {
            if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("Xbox Controller");
            return asset.controlSchemes[m_XboxControllerSchemeIndex];
        }
    }
    private int m_PS4ControllerSchemeIndex = -1;
    public InputControlScheme PS4ControllerScheme
    {
        get
        {
            if (m_PS4ControllerSchemeIndex == -1) m_PS4ControllerSchemeIndex = asset.FindControlSchemeIndex("PS4 Controller");
            return asset.controlSchemes[m_PS4ControllerSchemeIndex];
        }
    }
    private int m_KeyboardSecondarySchemeIndex = -1;
    public InputControlScheme KeyboardSecondaryScheme
    {
        get
        {
            if (m_KeyboardSecondarySchemeIndex == -1) m_KeyboardSecondarySchemeIndex = asset.FindControlSchemeIndex("KeyboardSecondary");
            return asset.controlSchemes[m_KeyboardSecondarySchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
    }
    public interface IGameMenuActions
    {
        void OnEnterGame(InputAction.CallbackContext context);
        void OnReady(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
}

// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/MenuInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MenuInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MenuInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MenuInputActions"",
    ""maps"": [
        {
            ""name"": ""SelectionMenu"",
            ""id"": ""52cb84a1-f75a-4ae5-845e-8d1860873cda"",
            ""actions"": [
                {
                    ""name"": ""Progress"",
                    ""type"": ""Button"",
                    ""id"": ""35ac6293-0251-45ed-8142-3f5ee1ac749f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""7369bece-d6fe-466c-b42c-0981b4e88e0d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeTeamLeft"",
                    ""type"": ""Button"",
                    ""id"": ""2603fe74-1eae-4a80-8569-fd5c75cec096"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeTeamRIght"",
                    ""type"": ""Button"",
                    ""id"": ""e5539960-466d-44bf-831a-8f33f0f331c8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeHeroForward"",
                    ""type"": ""Button"",
                    ""id"": ""de1b6ba9-73cf-421f-a319-a1dda0f180a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ChangeHeroBackward"",
                    ""type"": ""Button"",
                    ""id"": ""2725c684-4b03-44dc-92c6-e1ea68cd4939"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""76dfa99a-8352-47a2-a39f-77df5f6602ac"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Progress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9a4dad7-10b1-4a31-9d2a-e4e9a928617e"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Progress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""776cce60-05b4-4e8a-90ca-870782397aa7"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82ee933a-d852-43cd-9519-b24000f6e07e"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3601029c-9bf2-4bbd-886e-9a7fbb8a4e5b"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeTeamLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23b81423-273b-464c-900a-84b972c30c9f"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeTeamLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""677b9015-1e98-4a2d-b988-087e209b937b"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeTeamRIght"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08942815-6095-45b3-8162-96c22fa591f4"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeTeamRIght"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""adbcbece-3c91-4fa7-9a14-6426c9b11139"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHeroForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a546b71-08b2-4113-959e-23c207f046f1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHeroForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33ae1620-ef9f-4318-a865-5ec2936e89e5"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHeroForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dedab2f9-9506-44b5-aba8-f841675d0704"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHeroBackward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""555f8481-9838-47f2-978a-579a64e32cb9"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHeroBackward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3b889d7-0de0-43e8-bca5-6db3c3a07cc4"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHeroBackward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // SelectionMenu
        m_SelectionMenu = asset.FindActionMap("SelectionMenu", throwIfNotFound: true);
        m_SelectionMenu_Progress = m_SelectionMenu.FindAction("Progress", throwIfNotFound: true);
        m_SelectionMenu_Back = m_SelectionMenu.FindAction("Back", throwIfNotFound: true);
        m_SelectionMenu_ChangeTeamLeft = m_SelectionMenu.FindAction("ChangeTeamLeft", throwIfNotFound: true);
        m_SelectionMenu_ChangeTeamRIght = m_SelectionMenu.FindAction("ChangeTeamRIght", throwIfNotFound: true);
        m_SelectionMenu_ChangeHeroForward = m_SelectionMenu.FindAction("ChangeHeroForward", throwIfNotFound: true);
        m_SelectionMenu_ChangeHeroBackward = m_SelectionMenu.FindAction("ChangeHeroBackward", throwIfNotFound: true);
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

    // SelectionMenu
    private readonly InputActionMap m_SelectionMenu;
    private ISelectionMenuActions m_SelectionMenuActionsCallbackInterface;
    private readonly InputAction m_SelectionMenu_Progress;
    private readonly InputAction m_SelectionMenu_Back;
    private readonly InputAction m_SelectionMenu_ChangeTeamLeft;
    private readonly InputAction m_SelectionMenu_ChangeTeamRIght;
    private readonly InputAction m_SelectionMenu_ChangeHeroForward;
    private readonly InputAction m_SelectionMenu_ChangeHeroBackward;
    public struct SelectionMenuActions
    {
        private @MenuInputActions m_Wrapper;
        public SelectionMenuActions(@MenuInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Progress => m_Wrapper.m_SelectionMenu_Progress;
        public InputAction @Back => m_Wrapper.m_SelectionMenu_Back;
        public InputAction @ChangeTeamLeft => m_Wrapper.m_SelectionMenu_ChangeTeamLeft;
        public InputAction @ChangeTeamRIght => m_Wrapper.m_SelectionMenu_ChangeTeamRIght;
        public InputAction @ChangeHeroForward => m_Wrapper.m_SelectionMenu_ChangeHeroForward;
        public InputAction @ChangeHeroBackward => m_Wrapper.m_SelectionMenu_ChangeHeroBackward;
        public InputActionMap Get() { return m_Wrapper.m_SelectionMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SelectionMenuActions set) { return set.Get(); }
        public void SetCallbacks(ISelectionMenuActions instance)
        {
            if (m_Wrapper.m_SelectionMenuActionsCallbackInterface != null)
            {
                @Progress.started -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnProgress;
                @Progress.performed -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnProgress;
                @Progress.canceled -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnProgress;
                @Back.started -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnBack;
                @ChangeTeamLeft.started -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeTeamLeft;
                @ChangeTeamLeft.performed -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeTeamLeft;
                @ChangeTeamLeft.canceled -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeTeamLeft;
                @ChangeTeamRIght.started -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeTeamRIght;
                @ChangeTeamRIght.performed -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeTeamRIght;
                @ChangeTeamRIght.canceled -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeTeamRIght;
                @ChangeHeroForward.started -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeHeroForward;
                @ChangeHeroForward.performed -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeHeroForward;
                @ChangeHeroForward.canceled -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeHeroForward;
                @ChangeHeroBackward.started -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeHeroBackward;
                @ChangeHeroBackward.performed -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeHeroBackward;
                @ChangeHeroBackward.canceled -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeHeroBackward;
            }
            m_Wrapper.m_SelectionMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Progress.started += instance.OnProgress;
                @Progress.performed += instance.OnProgress;
                @Progress.canceled += instance.OnProgress;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @ChangeTeamLeft.started += instance.OnChangeTeamLeft;
                @ChangeTeamLeft.performed += instance.OnChangeTeamLeft;
                @ChangeTeamLeft.canceled += instance.OnChangeTeamLeft;
                @ChangeTeamRIght.started += instance.OnChangeTeamRIght;
                @ChangeTeamRIght.performed += instance.OnChangeTeamRIght;
                @ChangeTeamRIght.canceled += instance.OnChangeTeamRIght;
                @ChangeHeroForward.started += instance.OnChangeHeroForward;
                @ChangeHeroForward.performed += instance.OnChangeHeroForward;
                @ChangeHeroForward.canceled += instance.OnChangeHeroForward;
                @ChangeHeroBackward.started += instance.OnChangeHeroBackward;
                @ChangeHeroBackward.performed += instance.OnChangeHeroBackward;
                @ChangeHeroBackward.canceled += instance.OnChangeHeroBackward;
            }
        }
    }
    public SelectionMenuActions @SelectionMenu => new SelectionMenuActions(this);
    public interface ISelectionMenuActions
    {
        void OnProgress(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnChangeTeamLeft(InputAction.CallbackContext context);
        void OnChangeTeamRIght(InputAction.CallbackContext context);
        void OnChangeHeroForward(InputAction.CallbackContext context);
        void OnChangeHeroBackward(InputAction.CallbackContext context);
    }
}

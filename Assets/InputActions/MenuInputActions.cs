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
                    ""name"": ""ChangeHero"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f0aec1db-a19c-49d1-a7cc-4678ed80d744"",
                    ""expectedControlType"": ""Axis"",
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
                    ""name"": ""Keyboard"",
                    ""id"": ""17aab463-b73f-450f-b3dd-77f1ed374b8b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHero"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0f39958a-579e-4dba-9ee8-d44cfe7ee394"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHero"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""36e4b92d-03f4-44ed-ab03-886d98fb0e45"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHero"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Analog"",
                    ""id"": ""880c95a4-b4a3-47a4-a876-e3c93fd9bdf1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHero"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7748ed5d-5b88-4c84-afb4-fe64c0c9d73b"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHero"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a2c949f4-e577-4210-b6e2-c56273e8146b"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeHero"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // SelectionMenu
        m_SelectionMenu = asset.FindActionMap("SelectionMenu", throwIfNotFound: true);
        m_SelectionMenu_Progress = m_SelectionMenu.FindAction("Progress", throwIfNotFound: true);
        m_SelectionMenu_ChangeHero = m_SelectionMenu.FindAction("ChangeHero", throwIfNotFound: true);
        m_SelectionMenu_Back = m_SelectionMenu.FindAction("Back", throwIfNotFound: true);
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
    private readonly InputAction m_SelectionMenu_ChangeHero;
    private readonly InputAction m_SelectionMenu_Back;
    public struct SelectionMenuActions
    {
        private @MenuInputActions m_Wrapper;
        public SelectionMenuActions(@MenuInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Progress => m_Wrapper.m_SelectionMenu_Progress;
        public InputAction @ChangeHero => m_Wrapper.m_SelectionMenu_ChangeHero;
        public InputAction @Back => m_Wrapper.m_SelectionMenu_Back;
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
                @ChangeHero.started -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeHero;
                @ChangeHero.performed -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeHero;
                @ChangeHero.canceled -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnChangeHero;
                @Back.started -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_SelectionMenuActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_SelectionMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Progress.started += instance.OnProgress;
                @Progress.performed += instance.OnProgress;
                @Progress.canceled += instance.OnProgress;
                @ChangeHero.started += instance.OnChangeHero;
                @ChangeHero.performed += instance.OnChangeHero;
                @ChangeHero.canceled += instance.OnChangeHero;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public SelectionMenuActions @SelectionMenu => new SelectionMenuActions(this);
    public interface ISelectionMenuActions
    {
        void OnProgress(InputAction.CallbackContext context);
        void OnChangeHero(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
}

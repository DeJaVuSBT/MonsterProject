//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputPlayerControl.inputactions
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

public partial class @InputPlayerControl : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputPlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputPlayerControl"",
    ""maps"": [
        {
            ""name"": ""Input"",
            ""id"": ""cf72adef-a266-4f62-9855-8d8ec22e302e"",
            ""actions"": [
                {
                    ""name"": ""JoyStickLeft"",
                    ""type"": ""Button"",
                    ""id"": ""b4f81b1d-70f8-4d6e-a455-680734d62c4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""JoyStickRight"",
                    ""type"": ""Button"",
                    ""id"": ""4365e3e9-dbd4-4897-b33d-1b271060d55b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""JoyStickUp"",
                    ""type"": ""Button"",
                    ""id"": ""67c0e2d4-c07d-49bb-9fec-eff34801f11c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""JoyStickDown"",
                    ""type"": ""Button"",
                    ""id"": ""5661a76f-8fef-4673-82a0-d93f754fe9ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpeceButtonPress"",
                    ""type"": ""Button"",
                    ""id"": ""a4c87c5e-ebcf-42e1-96c8-4185cd9587b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e61bdb3c-0529-43b2-bacb-f5f23b9e0dd3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoyStickLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ec96098-a435-49c3-8576-43b053ac03f4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoyStickRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbff1835-24be-4d8c-b849-17067e39bab3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoyStickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e06de742-c702-4aa1-869b-bed4f917c84c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoyStickDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""510f6e78-eff6-43a9-8c3a-cf13e6191017"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpeceButtonPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Input
        m_Input = asset.FindActionMap("Input", throwIfNotFound: true);
        m_Input_JoyStickLeft = m_Input.FindAction("JoyStickLeft", throwIfNotFound: true);
        m_Input_JoyStickRight = m_Input.FindAction("JoyStickRight", throwIfNotFound: true);
        m_Input_JoyStickUp = m_Input.FindAction("JoyStickUp", throwIfNotFound: true);
        m_Input_JoyStickDown = m_Input.FindAction("JoyStickDown", throwIfNotFound: true);
        m_Input_SpeceButtonPress = m_Input.FindAction("SpeceButtonPress", throwIfNotFound: true);
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

    // Input
    private readonly InputActionMap m_Input;
    private IInputActions m_InputActionsCallbackInterface;
    private readonly InputAction m_Input_JoyStickLeft;
    private readonly InputAction m_Input_JoyStickRight;
    private readonly InputAction m_Input_JoyStickUp;
    private readonly InputAction m_Input_JoyStickDown;
    private readonly InputAction m_Input_SpeceButtonPress;
    public struct InputActions
    {
        private @InputPlayerControl m_Wrapper;
        public InputActions(@InputPlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @JoyStickLeft => m_Wrapper.m_Input_JoyStickLeft;
        public InputAction @JoyStickRight => m_Wrapper.m_Input_JoyStickRight;
        public InputAction @JoyStickUp => m_Wrapper.m_Input_JoyStickUp;
        public InputAction @JoyStickDown => m_Wrapper.m_Input_JoyStickDown;
        public InputAction @SpeceButtonPress => m_Wrapper.m_Input_SpeceButtonPress;
        public InputActionMap Get() { return m_Wrapper.m_Input; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputActions set) { return set.Get(); }
        public void SetCallbacks(IInputActions instance)
        {
            if (m_Wrapper.m_InputActionsCallbackInterface != null)
            {
                @JoyStickLeft.started -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickLeft;
                @JoyStickLeft.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickLeft;
                @JoyStickLeft.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickLeft;
                @JoyStickRight.started -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickRight;
                @JoyStickRight.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickRight;
                @JoyStickRight.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickRight;
                @JoyStickUp.started -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickUp;
                @JoyStickUp.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickUp;
                @JoyStickUp.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickUp;
                @JoyStickDown.started -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickDown;
                @JoyStickDown.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickDown;
                @JoyStickDown.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnJoyStickDown;
                @SpeceButtonPress.started -= m_Wrapper.m_InputActionsCallbackInterface.OnSpeceButtonPress;
                @SpeceButtonPress.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnSpeceButtonPress;
                @SpeceButtonPress.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnSpeceButtonPress;
            }
            m_Wrapper.m_InputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @JoyStickLeft.started += instance.OnJoyStickLeft;
                @JoyStickLeft.performed += instance.OnJoyStickLeft;
                @JoyStickLeft.canceled += instance.OnJoyStickLeft;
                @JoyStickRight.started += instance.OnJoyStickRight;
                @JoyStickRight.performed += instance.OnJoyStickRight;
                @JoyStickRight.canceled += instance.OnJoyStickRight;
                @JoyStickUp.started += instance.OnJoyStickUp;
                @JoyStickUp.performed += instance.OnJoyStickUp;
                @JoyStickUp.canceled += instance.OnJoyStickUp;
                @JoyStickDown.started += instance.OnJoyStickDown;
                @JoyStickDown.performed += instance.OnJoyStickDown;
                @JoyStickDown.canceled += instance.OnJoyStickDown;
                @SpeceButtonPress.started += instance.OnSpeceButtonPress;
                @SpeceButtonPress.performed += instance.OnSpeceButtonPress;
                @SpeceButtonPress.canceled += instance.OnSpeceButtonPress;
            }
        }
    }
    public InputActions @Input => new InputActions(this);
    public interface IInputActions
    {
        void OnJoyStickLeft(InputAction.CallbackContext context);
        void OnJoyStickRight(InputAction.CallbackContext context);
        void OnJoyStickUp(InputAction.CallbackContext context);
        void OnJoyStickDown(InputAction.CallbackContext context);
        void OnSpeceButtonPress(InputAction.CallbackContext context);
    }
}

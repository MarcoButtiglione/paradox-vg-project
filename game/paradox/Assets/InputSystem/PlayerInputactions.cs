//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSystem/PlayerInputactions.inputactions
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

public partial class @PlayerInputactions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputactions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputactions"",
    ""maps"": [
        {
            ""name"": ""YoungPlayer"",
            ""id"": ""815e2032-d5a9-4175-97d5-448b338a556a"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7cf13671-2b18-4fbe-aad8-d0f3cd04cf91"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3700c96b-37b5-495f-a229-84fe4d378328"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""OldPlayer"",
            ""id"": ""15d95dda-cc98-47f7-aa2c-6c67557e567c"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""84b3704b-bf86-48dd-b4d1-eabf76f2e311"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""40c21744-cd32-4786-87a8-4788121eeebe"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // YoungPlayer
        m_YoungPlayer = asset.FindActionMap("YoungPlayer", throwIfNotFound: true);
        m_YoungPlayer_Jump = m_YoungPlayer.FindAction("Jump", throwIfNotFound: true);
        // OldPlayer
        m_OldPlayer = asset.FindActionMap("OldPlayer", throwIfNotFound: true);
        m_OldPlayer_Jump = m_OldPlayer.FindAction("Jump", throwIfNotFound: true);
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

    // YoungPlayer
    private readonly InputActionMap m_YoungPlayer;
    private IYoungPlayerActions m_YoungPlayerActionsCallbackInterface;
    private readonly InputAction m_YoungPlayer_Jump;
    public struct YoungPlayerActions
    {
        private @PlayerInputactions m_Wrapper;
        public YoungPlayerActions(@PlayerInputactions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_YoungPlayer_Jump;
        public InputActionMap Get() { return m_Wrapper.m_YoungPlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(YoungPlayerActions set) { return set.Get(); }
        public void SetCallbacks(IYoungPlayerActions instance)
        {
            if (m_Wrapper.m_YoungPlayerActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_YoungPlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_YoungPlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_YoungPlayerActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_YoungPlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public YoungPlayerActions @YoungPlayer => new YoungPlayerActions(this);

    // OldPlayer
    private readonly InputActionMap m_OldPlayer;
    private IOldPlayerActions m_OldPlayerActionsCallbackInterface;
    private readonly InputAction m_OldPlayer_Jump;
    public struct OldPlayerActions
    {
        private @PlayerInputactions m_Wrapper;
        public OldPlayerActions(@PlayerInputactions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_OldPlayer_Jump;
        public InputActionMap Get() { return m_Wrapper.m_OldPlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OldPlayerActions set) { return set.Get(); }
        public void SetCallbacks(IOldPlayerActions instance)
        {
            if (m_Wrapper.m_OldPlayerActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_OldPlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_OldPlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_OldPlayerActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_OldPlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public OldPlayerActions @OldPlayer => new OldPlayerActions(this);
    public interface IYoungPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IOldPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
    }
}
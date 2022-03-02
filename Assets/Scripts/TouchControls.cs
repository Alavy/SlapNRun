// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/TouchControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TouchControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TouchControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchControls"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""ce6a07e0-ad6d-4a26-b413-bc68e541b1df"",
            ""actions"": [
                {
                    ""name"": ""PrimaryContract"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5adea40b-7d19-4602-8547-16724bdf21ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PrimaryPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d23bbf1d-dd02-4e6b-b324-ec5d6254cda4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""1320e5d6-2171-494d-b5a8-7207759682a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""99831cd5-3a2e-47bd-8cbe-4e276a2c51bc"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6355f447-08d9-48bf-ab03-d5bf8a82c067"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryContract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33e8a1f4-2f5d-43da-9b09-4f15b6b641e1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_PrimaryContract = m_Touch.FindAction("PrimaryContract", throwIfNotFound: true);
        m_Touch_PrimaryPosition = m_Touch.FindAction("PrimaryPosition", throwIfNotFound: true);
        m_Touch_Test = m_Touch.FindAction("Test", throwIfNotFound: true);
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

    // Touch
    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_PrimaryContract;
    private readonly InputAction m_Touch_PrimaryPosition;
    private readonly InputAction m_Touch_Test;
    public struct TouchActions
    {
        private @TouchControls m_Wrapper;
        public TouchActions(@TouchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryContract => m_Wrapper.m_Touch_PrimaryContract;
        public InputAction @PrimaryPosition => m_Wrapper.m_Touch_PrimaryPosition;
        public InputAction @Test => m_Wrapper.m_Touch_Test;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                @PrimaryContract.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryContract;
                @PrimaryContract.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryContract;
                @PrimaryContract.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryContract;
                @PrimaryPosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryPosition;
                @Test.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTest;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryContract.started += instance.OnPrimaryContract;
                @PrimaryContract.performed += instance.OnPrimaryContract;
                @PrimaryContract.canceled += instance.OnPrimaryContract;
                @PrimaryPosition.started += instance.OnPrimaryPosition;
                @PrimaryPosition.performed += instance.OnPrimaryPosition;
                @PrimaryPosition.canceled += instance.OnPrimaryPosition;
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
            }
        }
    }
    public TouchActions @Touch => new TouchActions(this);
    public interface ITouchActions
    {
        void OnPrimaryContract(InputAction.CallbackContext context);
        void OnPrimaryPosition(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
    }
}

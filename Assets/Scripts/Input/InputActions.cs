//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/Input/InputActions.inputactions
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

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""GameInput"",
            ""id"": ""651a83c5-0af3-4f81-979c-e54237ed98bc"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f00a4e1f-196b-465f-8a58-cd8ec5f1ab75"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraLock"",
                    ""type"": ""Value"",
                    ""id"": ""65bfd50f-9783-4e1e-bf93-0037b7a874a1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""a44fd8c1-19f9-467a-9891-d3676fdfe6c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LAttack"",
                    ""type"": ""Button"",
                    ""id"": ""dae04aee-a00f-4dbd-8330-5765488cd60d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RAttack"",
                    ""type"": ""Button"",
                    ""id"": ""4032d018-5e08-4b57-8873-51319abdad00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Climb"",
                    ""type"": ""Button"",
                    ""id"": ""b6a8f8b1-e816-40fb-a7fe-4dde7180e6ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""bd25ad58-cf8a-4ac5-85fb-5c0d336adf1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Takeout"",
                    ""type"": ""Button"",
                    ""id"": ""0d5061f6-7dae-495e-b8d2-f4259679bf07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6cff74ba-d5e7-44b3-b3b4-6954d284e5ac"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""20c9640a-8155-43d3-ac9e-3a477841f660"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a42d74fc-329b-4f09-be49-d6647b21fe56"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5e28b85f-711b-4904-b1b3-d211c8eb3036"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""76093063-9c54-46fd-ba4c-d85c2f2b2b99"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a7be242d-cd12-4ac4-9867-2474a4bb29ec"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.15,y=0.15)"",
                    ""groups"": """",
                    ""action"": ""CameraLock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0d3b8bc-a011-4894-96e4-133da6078a5c"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49ea539a-8aca-4a41-974c-170a803a701e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""405a1834-7ee2-461f-93ba-8afa7979ed3a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd811ec2-9bd2-4a18-804b-ca407814920a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b46e064-ee47-4db4-8510-e42726a2d07e"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a13a6ef7-583e-4831-beed-d5aa397e983c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Takeout"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GameInput
        m_GameInput = asset.FindActionMap("GameInput", throwIfNotFound: true);
        m_GameInput_Movement = m_GameInput.FindAction("Movement", throwIfNotFound: true);
        m_GameInput_CameraLock = m_GameInput.FindAction("CameraLock", throwIfNotFound: true);
        m_GameInput_Run = m_GameInput.FindAction("Run", throwIfNotFound: true);
        m_GameInput_LAttack = m_GameInput.FindAction("LAttack", throwIfNotFound: true);
        m_GameInput_RAttack = m_GameInput.FindAction("RAttack", throwIfNotFound: true);
        m_GameInput_Climb = m_GameInput.FindAction("Climb", throwIfNotFound: true);
        m_GameInput_Grab = m_GameInput.FindAction("Grab", throwIfNotFound: true);
        m_GameInput_Takeout = m_GameInput.FindAction("Takeout", throwIfNotFound: true);
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

    // GameInput
    private readonly InputActionMap m_GameInput;
    private List<IGameInputActions> m_GameInputActionsCallbackInterfaces = new List<IGameInputActions>();
    private readonly InputAction m_GameInput_Movement;
    private readonly InputAction m_GameInput_CameraLock;
    private readonly InputAction m_GameInput_Run;
    private readonly InputAction m_GameInput_LAttack;
    private readonly InputAction m_GameInput_RAttack;
    private readonly InputAction m_GameInput_Climb;
    private readonly InputAction m_GameInput_Grab;
    private readonly InputAction m_GameInput_Takeout;
    public struct GameInputActions
    {
        private @InputActions m_Wrapper;
        public GameInputActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_GameInput_Movement;
        public InputAction @CameraLock => m_Wrapper.m_GameInput_CameraLock;
        public InputAction @Run => m_Wrapper.m_GameInput_Run;
        public InputAction @LAttack => m_Wrapper.m_GameInput_LAttack;
        public InputAction @RAttack => m_Wrapper.m_GameInput_RAttack;
        public InputAction @Climb => m_Wrapper.m_GameInput_Climb;
        public InputAction @Grab => m_Wrapper.m_GameInput_Grab;
        public InputAction @Takeout => m_Wrapper.m_GameInput_Takeout;
        public InputActionMap Get() { return m_Wrapper.m_GameInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameInputActions set) { return set.Get(); }
        public void AddCallbacks(IGameInputActions instance)
        {
            if (instance == null || m_Wrapper.m_GameInputActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameInputActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @CameraLock.started += instance.OnCameraLock;
            @CameraLock.performed += instance.OnCameraLock;
            @CameraLock.canceled += instance.OnCameraLock;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @LAttack.started += instance.OnLAttack;
            @LAttack.performed += instance.OnLAttack;
            @LAttack.canceled += instance.OnLAttack;
            @RAttack.started += instance.OnRAttack;
            @RAttack.performed += instance.OnRAttack;
            @RAttack.canceled += instance.OnRAttack;
            @Climb.started += instance.OnClimb;
            @Climb.performed += instance.OnClimb;
            @Climb.canceled += instance.OnClimb;
            @Grab.started += instance.OnGrab;
            @Grab.performed += instance.OnGrab;
            @Grab.canceled += instance.OnGrab;
            @Takeout.started += instance.OnTakeout;
            @Takeout.performed += instance.OnTakeout;
            @Takeout.canceled += instance.OnTakeout;
        }

        private void UnregisterCallbacks(IGameInputActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @CameraLock.started -= instance.OnCameraLock;
            @CameraLock.performed -= instance.OnCameraLock;
            @CameraLock.canceled -= instance.OnCameraLock;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @LAttack.started -= instance.OnLAttack;
            @LAttack.performed -= instance.OnLAttack;
            @LAttack.canceled -= instance.OnLAttack;
            @RAttack.started -= instance.OnRAttack;
            @RAttack.performed -= instance.OnRAttack;
            @RAttack.canceled -= instance.OnRAttack;
            @Climb.started -= instance.OnClimb;
            @Climb.performed -= instance.OnClimb;
            @Climb.canceled -= instance.OnClimb;
            @Grab.started -= instance.OnGrab;
            @Grab.performed -= instance.OnGrab;
            @Grab.canceled -= instance.OnGrab;
            @Takeout.started -= instance.OnTakeout;
            @Takeout.performed -= instance.OnTakeout;
            @Takeout.canceled -= instance.OnTakeout;
        }

        public void RemoveCallbacks(IGameInputActions instance)
        {
            if (m_Wrapper.m_GameInputActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameInputActions instance)
        {
            foreach (var item in m_Wrapper.m_GameInputActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameInputActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameInputActions @GameInput => new GameInputActions(this);
    public interface IGameInputActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCameraLock(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnLAttack(InputAction.CallbackContext context);
        void OnRAttack(InputAction.CallbackContext context);
        void OnClimb(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnTakeout(InputAction.CallbackContext context);
    }
}
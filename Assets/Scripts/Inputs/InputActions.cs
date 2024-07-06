//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Inputs/InputActions.inputactions
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
            ""name"": ""Game"",
            ""id"": ""a65b1338-3bc6-42cd-873b-d576330142b5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b66fc51c-b9d9-4919-9c3f-643c2e01c68d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WSAD"",
                    ""id"": ""56614c2a-6bae-4294-9e5d-28babed90bcf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""964f83d2-199a-4632-93e4-0bbc1b4e1c72"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ccbd3c43-8bef-4b98-9018-bcc6653eab4d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d3aef92f-2d25-4110-a0bc-9f740395b195"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""87dbbfb4-b8da-4306-be6c-c64fff63b354"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Workshop"",
            ""id"": ""9175282a-2b8a-44e6-a572-ec7cae0c7a06"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""b1fc785e-3657-4054-9b8e-7079f1e7205b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""Button"",
                    ""id"": ""9abe135d-19b7-4652-9782-90f4328a5892"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Climb"",
                    ""type"": ""Value"",
                    ""id"": ""375ca3d8-c696-4c10-a972-e2dd2559d611"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""55f2d55c-fdb9-475e-bd10-ff120f66625b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=-100,y=100)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e2c1200-18bc-4752-a03d-4ef3305d6f82"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""DeltaTimeScale,ScaleVector2(x=0.1,y=0.1)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dedbd741-d010-41f3-9b14-8c155fce1aaa"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d291076c-f147-4ee3-ab68-b2e013f0a5bb"",
                    ""path"": ""<XInputController>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""737cbe22-580e-43b8-898b-082ec1d99ff8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Move = m_Game.FindAction("Move", throwIfNotFound: true);
        // Workshop
        m_Workshop = asset.FindActionMap("Workshop", throwIfNotFound: true);
        m_Workshop_Look = m_Workshop.FindAction("Look", throwIfNotFound: true);
        m_Workshop_Hold = m_Workshop.FindAction("Hold", throwIfNotFound: true);
        m_Workshop_Climb = m_Workshop.FindAction("Climb", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private List<IGameActions> m_GameActionsCallbackInterfaces = new List<IGameActions>();
    private readonly InputAction m_Game_Move;
    public struct GameActions
    {
        private @InputActions m_Wrapper;
        public GameActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Game_Move;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void AddCallbacks(IGameActions instance)
        {
            if (instance == null || m_Wrapper.m_GameActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IGameActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameActions instance)
        {
            foreach (var item in m_Wrapper.m_GameActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameActions @Game => new GameActions(this);

    // Workshop
    private readonly InputActionMap m_Workshop;
    private List<IWorkshopActions> m_WorkshopActionsCallbackInterfaces = new List<IWorkshopActions>();
    private readonly InputAction m_Workshop_Look;
    private readonly InputAction m_Workshop_Hold;
    private readonly InputAction m_Workshop_Climb;
    public struct WorkshopActions
    {
        private @InputActions m_Wrapper;
        public WorkshopActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Workshop_Look;
        public InputAction @Hold => m_Wrapper.m_Workshop_Hold;
        public InputAction @Climb => m_Wrapper.m_Workshop_Climb;
        public InputActionMap Get() { return m_Wrapper.m_Workshop; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WorkshopActions set) { return set.Get(); }
        public void AddCallbacks(IWorkshopActions instance)
        {
            if (instance == null || m_Wrapper.m_WorkshopActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_WorkshopActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Hold.started += instance.OnHold;
            @Hold.performed += instance.OnHold;
            @Hold.canceled += instance.OnHold;
            @Climb.started += instance.OnClimb;
            @Climb.performed += instance.OnClimb;
            @Climb.canceled += instance.OnClimb;
        }

        private void UnregisterCallbacks(IWorkshopActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Hold.started -= instance.OnHold;
            @Hold.performed -= instance.OnHold;
            @Hold.canceled -= instance.OnHold;
            @Climb.started -= instance.OnClimb;
            @Climb.performed -= instance.OnClimb;
            @Climb.canceled -= instance.OnClimb;
        }

        public void RemoveCallbacks(IWorkshopActions instance)
        {
            if (m_Wrapper.m_WorkshopActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IWorkshopActions instance)
        {
            foreach (var item in m_Wrapper.m_WorkshopActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_WorkshopActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public WorkshopActions @Workshop => new WorkshopActions(this);
    public interface IGameActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IWorkshopActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnHold(InputAction.CallbackContext context);
        void OnClimb(InputAction.CallbackContext context);
    }
}
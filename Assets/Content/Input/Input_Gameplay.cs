// GENERATED AUTOMATICALLY FROM 'Assets/Content/Input/Input_Gameplay.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input_Gameplay : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input_Gameplay()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input_Gameplay"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""605281f2-2034-4862-8ca9-55d7fa6e3993"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""07184cb4-5125-4b34-9af0-7ba7dddb15df"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cb5fc6c8-ea31-49aa-af96-14593116d7d0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""0e811814-1005-43af-95b1-7dc94504fc53"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""a026e47c-f2ba-47a8-9fa8-cbaa2d6e1752"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AimMouse"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0adbb42b-44b0-4211-9a8b-f67812af5cbc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7ae7f9fb-5830-4c23-9f2f-cff5e02479f8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""515f3ae6-86c9-449d-a32e-b44ea884d242"",
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
                    ""id"": ""1e6b3dcf-0196-471d-9889-28a31af7dd55"",
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
                    ""id"": ""070b3eac-0e04-437e-9dc8-2e71f19a1ec1"",
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
                    ""id"": ""586be0ed-043c-44df-a5c4-4ec08bc7a202"",
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
                    ""id"": ""be0e32eb-e359-4aff-a141-a91341d5c31d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d52cc8b7-2528-4897-8604-fe85e596bbba"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ee2d51e-ff35-4715-bc8c-630951388d3c"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""949fc75e-de5e-4235-b99f-45c6e5df7852"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9865b92-b435-49f6-bbec-7a3960a96147"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbc56747-466b-4217-ae16-afc2fcb8e6b8"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Move = m_GamePlay.FindAction("Move", throwIfNotFound: true);
        m_GamePlay_Aim = m_GamePlay.FindAction("Aim", throwIfNotFound: true);
        m_GamePlay_Fire = m_GamePlay.FindAction("Fire", throwIfNotFound: true);
        m_GamePlay_Exit = m_GamePlay.FindAction("Exit", throwIfNotFound: true);
        m_GamePlay_AimMouse = m_GamePlay.FindAction("AimMouse", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Move;
    private readonly InputAction m_GamePlay_Aim;
    private readonly InputAction m_GamePlay_Fire;
    private readonly InputAction m_GamePlay_Exit;
    private readonly InputAction m_GamePlay_AimMouse;
    public struct GamePlayActions
    {
        private @Input_Gameplay m_Wrapper;
        public GamePlayActions(@Input_Gameplay wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GamePlay_Move;
        public InputAction @Aim => m_Wrapper.m_GamePlay_Aim;
        public InputAction @Fire => m_Wrapper.m_GamePlay_Fire;
        public InputAction @Exit => m_Wrapper.m_GamePlay_Exit;
        public InputAction @AimMouse => m_Wrapper.m_GamePlay_AimMouse;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Aim.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAim;
                @Fire.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnFire;
                @Exit.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnExit;
                @AimMouse.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAimMouse;
                @AimMouse.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAimMouse;
                @AimMouse.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAimMouse;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
                @AimMouse.started += instance.OnAimMouse;
                @AimMouse.performed += instance.OnAimMouse;
                @AimMouse.canceled += instance.OnAimMouse;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    public interface IGamePlayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
        void OnAimMouse(InputAction.CallbackContext context);
    }
}

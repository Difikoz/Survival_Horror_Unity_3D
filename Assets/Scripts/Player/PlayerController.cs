using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PlayerCrouchComponent))]
    [RequireComponent(typeof(PlayerJumpComponent))]
    [RequireComponent(typeof(PlayerInputComponent))]
    [RequireComponent(typeof(PlayerLookComponent))]
    [RequireComponent(typeof(PlayerMovementComponent))]
    [RequireComponent(typeof(PlayerSlideComponent))]
    [RequireComponent(typeof(PlayerSlopeComponent))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController _cc;
        private PlayerCrouchComponent _crouchComponent;
        private PlayerJumpComponent _jumpComponent;
        private PlayerInputComponent _inputComponent;
        private PlayerLookComponent _lookComponent;
        private PlayerMovementComponent _movementComponent;
        private PlayerSlideComponent _slideComponent;
        private PlayerSlopeComponent _slopeComponent;

        public CharacterController CC => _cc;
        public PlayerCrouchComponent CrouchComponent => _crouchComponent;
        public PlayerJumpComponent JumpComponent => _jumpComponent;
        public PlayerInputComponent InputComponent => _inputComponent;
        public PlayerLookComponent LookComponent => _lookComponent;
        public PlayerMovementComponent MovementComponent => _movementComponent;
        public PlayerSlideComponent SlideComponent => _slideComponent;
        public PlayerSlopeComponent SlopeComponent => _slopeComponent;

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
            _crouchComponent = GetComponent<PlayerCrouchComponent>();
            _jumpComponent = GetComponent<PlayerJumpComponent>();
            _inputComponent = GetComponent<PlayerInputComponent>();
            _lookComponent = GetComponent<PlayerLookComponent>();
            _movementComponent = GetComponent<PlayerMovementComponent>();
            _slideComponent = GetComponent<PlayerSlideComponent>();
            _slopeComponent = GetComponent<PlayerSlopeComponent>();
            _crouchComponent.Initialize();
            _jumpComponent.Initialize();
            _inputComponent.Initialize();
            _lookComponent.Initialize();
            _movementComponent.Initialize();
            _slideComponent.Initialize();
            _slopeComponent.Initialize();
        }

        private void OnEnable()
        {
            _crouchComponent.Enable();
            _jumpComponent.Enable();
            _inputComponent.Enable();
            _lookComponent.Enable();
            _movementComponent.Enable();
            _slideComponent.Enable();
            _slopeComponent.Enable();
        }

        private void OnDisable()
        {
            _crouchComponent.Disable();
            _jumpComponent.Disable();
            _inputComponent.Disable();
            _lookComponent.Disable();
            _movementComponent.Disable();
            _slideComponent.Disable();
            _slopeComponent.Disable();
        }

        private void Update()
        {
            _crouchComponent.OnUpdate();
            _jumpComponent.OnUpdate();
            _inputComponent.OnUpdate();
            _lookComponent.OnUpdate();
            _movementComponent.OnUpdate();
            _slideComponent.OnUpdate();
            _slopeComponent.OnUpdate();
        }

        private void LateUpdate()
        {
            _lookComponent.OnLateUpdate();
        }
    }
}
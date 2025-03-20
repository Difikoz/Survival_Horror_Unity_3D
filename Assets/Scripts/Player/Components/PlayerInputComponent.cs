using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInputComponent : PlayerComponent
    {
        private PlayerInputActions _inputActions;

        [Header("Debug")]
        [SerializeField] private Vector2 _moveInput;
        [SerializeField] private Vector2 _lookInput;

        public Vector2 MoveInput => _moveInput;
        public Vector2 LookInput => _lookInput;

        public override void Initialize()
        {
            base.Initialize();
            _inputActions = new();
            HideCursor();
        }

        public override void Enable()
        {
            base.Enable();
            _inputActions.Enable();
            _inputActions.Player.Sprint.performed += ctx => _player.MovementComponent.ToggleSprint();
            _inputActions.Player.Crouch.performed += ctx => _player.CrouchComponent.ToggleCrouch();
            _inputActions.Player.Jump.performed += ctx => _player.JumpComponent.StartJump();
            _inputActions.Player.Jump.canceled += ctx => _player.JumpComponent.StopJump();
        }

        public override void Disable()
        {
            _inputActions.Player.Sprint.performed -= ctx => _player.MovementComponent.ToggleSprint();
            _inputActions.Player.Crouch.performed -= ctx => _player.CrouchComponent.ToggleCrouch();
            _inputActions.Player.Jump.performed -= ctx => _player.JumpComponent.StartJump();
            _inputActions.Player.Jump.canceled -= ctx => _player.JumpComponent.StopJump();
            _inputActions.Disable();
            base.Disable();
        }

        public override void OnUpdate()
        {
            if (!_enabled)
            {
                return;
            }
            base.OnUpdate();
            _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            _lookInput = _inputActions.Player.Look.ReadValue<Vector2>();
        }

        public void ToggleCursor()
        {

        }

        public void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
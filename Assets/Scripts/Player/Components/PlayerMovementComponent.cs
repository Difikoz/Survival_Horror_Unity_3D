using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerMovementComponent : PlayerComponent
    {
        public Action OnStartSprint;
        public Action OnStopSprint;

        [SerializeField] private PlayerMovementConfig _crouchConfig;
        [SerializeField] private PlayerMovementConfig _walkConfig;
        [SerializeField] private PlayerMovementConfig _sprintConfig;

        [Header("Debug")]
        [SerializeField] private PlayerMovementConfig _currentConfig;
        [SerializeField] private Vector3 _velocity;
        [SerializeField] private bool _isSprinting;

        public PlayerMovementConfig Config => _currentConfig;
        public Vector3 Velocity => _velocity;
        public bool IsSprinting => _isSprinting;

        public override void Enable()
        {
            base.Enable();
            _player.CrouchComponent.OnStartCrouch += OnStartCrouch;
            _player.CrouchComponent.OnStopCrouch += OnStopCrouch;
            StopSprint();
        }

        public override void Disable()
        {
            _player.CrouchComponent.OnStartCrouch -= OnStartCrouch;
            _player.CrouchComponent.OnStopCrouch -= OnStopCrouch;
            base.Disable();
        }

        public override void OnUpdate()
        {
            if (!_enabled)
            {
                return;
            }
            base.OnUpdate();
            CalculateMoveVelocity();
            _player.CC.Move(_velocity * Time.deltaTime);
        }

        private void CalculateMoveVelocity()
        {
            if (_player.SlideComponent.IsSliding || _player.SlopeComponent.IsSloping || ObstacleOnWay())
            {
                _velocity = Vector3.zero;
            }
            else if (_player.InputComponent.MoveInput != Vector2.zero)
            {
                _velocity = Vector3.MoveTowards(_velocity, GetMoveDirection() * _currentConfig.MaxSpeed, _currentConfig.Acceleration * (_player.JumpComponent.IsGrounded ? 1f : _currentConfig.AirControl) * Time.deltaTime);
            }
            else
            {
                _velocity = Vector3.MoveTowards(_velocity, Vector3.zero, _currentConfig.Deceleration * (_player.JumpComponent.IsGrounded ? 1f : _currentConfig.AirControl) * Time.deltaTime);
                if (_isSprinting && _velocity.magnitude < 0.1f)
                {
                    StopSprint();
                }
            }
        }

        private Vector3 GetMoveDirection()
        {
            return transform.forward * _player.InputComponent.MoveInput.y + transform.right * _player.InputComponent.MoveInput.x;
        }

        public void ToggleSprint()
        {
            if (_isSprinting)
            {
                StopSprint();
            }
            else
            {
                StartSprint();
            }
        }

        private void StartSprint()
        {
            if (_player.SlopeComponent.IsSloping || _player.InputComponent.MoveInput.magnitude < 0.1f)
            {
                return;
            }
            if (_player.CrouchComponent.IsCrouching && !_player.CrouchComponent.StopCrouch())
            {
                return;
            }
            _isSprinting = true;
            _currentConfig = _sprintConfig;
            OnStartSprint?.Invoke();
        }

        private void StopSprint()
        {
            _isSprinting = false;
            _currentConfig = _walkConfig;
            OnStopSprint?.Invoke();
        }
        private bool ObstacleOnWay()
        {
            return _velocity.magnitude > 0f && Physics.CapsuleCast(transform.position + Vector3.up * _player.CC.height * 0.3f, transform.position + Vector3.up * _player.CC.height * 0.7f, _player.CC.radius * 0.5f, _velocity.normalized, _player.CC.radius * 0.75f, _currentConfig.ObstacleMask);
        }

        private void OnStartCrouch()
        {
            if (_isSprinting && _player.InputComponent.MoveInput.magnitude > 0.5f)
            {
                _player.SlideComponent.StartSlide();
            }
            StopSprint();
            _currentConfig = _crouchConfig;
        }

        private void OnStopCrouch()
        {
            if (_isSprinting)
            {
                StartSprint();
            }
            else
            {
                StopSprint();
            }
        }

        private void OnDrawGizmos()
        {
            if (_player != null && _player.CC != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position + Vector3.up * _player.CC.radius / 2f, _player.CC.radius);
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position + Vector3.up * _player.CC.height * 0.3f + _velocity.normalized * _player.CC.radius * 0.75f, _player.CC.radius * 0.5f);
                Gizmos.DrawWireSphere(transform.position + Vector3.up * _player.CC.height * 0.7f + _velocity.normalized * _player.CC.radius * 0.75f, _player.CC.radius * 0.5f);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position + Vector3.up * _player.CC.height - Vector3.up * _player.CC.radius * 0.95f, _player.CC.radius);
            }
        }
    }
}
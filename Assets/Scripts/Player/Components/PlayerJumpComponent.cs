using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerJumpComponent : PlayerComponent
    {
        public Action OnGrounded;
        public Action OnFall;
        public Action OnJump;

        [SerializeField] private PlayerJumpConfig _config;

        [Header("Debug")]
        [SerializeField] private Vector3 _velocity;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _groundedTime;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private int _jumpCount;
        private RaycastHit _groundHit;

        public PlayerJumpConfig Config => _config;
        public bool IsGrounded => _isGrounded;

        public override void OnUpdate()
        {
            if (!_enabled)
            {
                return;
            }
            base.OnUpdate();
            CheckJump();
            CheckGround();
            CalculateFallVelocity();
            _player.CC.Move(_velocity * Time.deltaTime);
        }

        private void CheckJump()
        {
            if (_jumpTime > 0f)
            {
                if (_groundedTime > 0f && !_player.SlopeComponent.IsSloping)
                {
                    ApplyJumpForce();
                }
                _jumpTime -= Time.deltaTime;
            }
        }

        private void CheckGround()
        {
            if (_isGrounded)
            {
                if (_velocity.y > 0f || !Physics.SphereCast(transform.position + _player.CC.center, _player.CC.radius, Vector3.down, out _groundHit, _player.CC.center.y - (_player.CC.radius * 0.8f), _config.ObstacleMask))
                {
                    _isGrounded = false;
                    OnFall?.Invoke();
                }
            }
            else
            {
                if (_velocity.y <= 0f && Physics.SphereCast(transform.position + _player.CC.center, _player.CC.radius, Vector3.down, out _groundHit, _player.CC.center.y - (_player.CC.radius * 0.8f), _config.ObstacleMask))
                {
                    _isGrounded = true;
                    OnGrounded?.Invoke();
                }
            }
        }

        private void CalculateFallVelocity()
        {
            if (_isGrounded)
            {
                _velocity.y = _config.Gravity / 10f;
                _groundedTime = _config.TimeToFall;
                _jumpCount = 0;
            }
            else
            {
                if (_velocity.y > 0f && UnderRoof())
                {
                    _velocity.y = 0f;
                }
                _velocity.y += _config.Gravity * Time.deltaTime;
                if (_groundedTime > 0f)
                {
                    _groundedTime -= Time.deltaTime;
                }
            }
        }

        public void StartJump()
        {
            if (_player.SlopeComponent.IsSloping || UnderRoof())
            {
                return;
            }
            if (_jumpCount == 0 || _jumpCount >= _config.JumpCount)
            {
                _jumpTime = _config.TimeToJump;
            }
            else
            {
                ApplyJumpForce();
            }
        }

        public void StopJump()
        {
            if (_velocity.y > 0f)
            {
                _velocity.y /= 2f;
            }
        }

        private void ApplyJumpForce()
        {
            _velocity.y = Mathf.Sqrt((_player.CrouchComponent.IsCrouching ? _config.CrouchJumpForce : _config.StandJumpForce) * -2f * _config.Gravity);
            _jumpTime = 0f;
            _groundedTime = 0f;
            _jumpCount++;
            OnJump?.Invoke();
        }

        public bool UnderRoof()
        {
            return Physics.SphereCast(transform.position + _player.CC.center, _player.CC.radius, Vector3.up, out _, _player.CC.center.y - (_player.CC.radius * 0.8f), _config.ObstacleMask);
        }
    }
}
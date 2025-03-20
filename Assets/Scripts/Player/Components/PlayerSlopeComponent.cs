using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerSlopeComponent : PlayerComponent
    {
        public Action OnStartSlope;
        public Action OnStopSlope;

        [SerializeField] private PlayerSlopeConfig _config;

        [Header("Debug")]
        [SerializeField] private Vector3 _velocity;
        private RaycastHit _groundHit;
        [SerializeField] private float _currentSpeed;
        [SerializeField] private float _lastSpeed;
        [SerializeField] private float _angle;
        [SerializeField] private bool _isSloping;

        public PlayerSlopeConfig Config => _config;
        public bool IsSloping => _isSloping;

        public override void Enable()
        {
            base.Enable();
            _player.CC.slopeLimit = _config.SlopeLimit;
        }

        public override void OnUpdate()
        {
            if (!_enabled)
            {
                return;
            }
            base.OnUpdate();
            if (Physics.SphereCast(transform.position + _player.CC.center, _player.CC.radius, Vector3.down, out _groundHit, _player.CC.center.y - (_player.CC.radius * 0.8f), _config.ObstacleMask))
            {
                _angle = Vector3.Angle(_groundHit.normal, Vector3.up);
                if (_angle >= _config.SlopeLimit)
                {
                    StartSlope();
                    return;
                }
            }
            StopSlope();

        }

        private void StartSlope()
        {
            _currentSpeed += _config.Gravity * Time.deltaTime;
            _lastSpeed = _currentSpeed;
            _velocity = Vector3.ProjectOnPlane(new Vector3(0f, _currentSpeed, 0f), _groundHit.normal);
            if (!_isSloping)
            {
                _isSloping = true;
                OnStartSlope?.Invoke();
            }
            _player.CC.Move(_velocity * Time.deltaTime);
        }

        private void StopSlope()
        {
            if (_currentSpeed != 0f)
            {
                _velocity = Vector3.ProjectOnPlane(new Vector3(0f, _currentSpeed, 0f), _groundHit.normal);
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0f, Math.Abs(_lastSpeed * _config.Gravity) / _config.TimeToStop * Time.deltaTime);
                _player.CC.Move(_velocity * Time.deltaTime);
                return;
            }
            if (_isSloping)
            {
                //_velocity = Vector3.zero;
                _isSloping = false;
                OnStopSlope?.Invoke();
            }
        }
    }
}
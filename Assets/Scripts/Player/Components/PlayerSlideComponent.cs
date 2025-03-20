using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerSlideComponent : PlayerComponent
    {
        public Action OnStartSlide;
        public Action OnStopSlide;

        [SerializeField] private PlayerSlideConfig _config;

        [Header("Debug")]
        [SerializeField] private Vector3 _startingVelocity;
        [SerializeField] private Vector3 _velocity;
        [SerializeField] private bool _isSliding;

        public PlayerSlideConfig Config => _config;
        public bool IsSliding => _isSliding;

        public override void OnUpdate()
        {
            if (!_enabled)
            {
                return;
            }
            base.OnUpdate();
            if (_velocity != Vector3.zero)
            {
                _velocity = Vector3.MoveTowards(_velocity, Vector3.zero, _startingVelocity.magnitude / _config.TimeToStop * Time.deltaTime);
                if (_velocity.magnitude < 0.1f)
                {
                    StopSlide();
                }
            }
            _player.CC.Move(_velocity * Time.deltaTime);
        }

        public void StartSlide()
        {
            if (!_enabled)
            {
                return;
            }
            _startingVelocity = _player.MovementComponent.Velocity;
            _velocity = _startingVelocity * _config.ForceMultiplier;
            _isSliding = true;
            OnStartSlide?.Invoke();
        }

        private void StopSlide()
        {
            _velocity = Vector3.zero;
            _isSliding = false;
            OnStopSlide?.Invoke();
        }
    }
}
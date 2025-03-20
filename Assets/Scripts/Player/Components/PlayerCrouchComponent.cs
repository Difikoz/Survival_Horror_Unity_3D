using System;
using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerCrouchComponent : PlayerComponent
    {
        public Action OnStartCrouch;
        public Action OnStopCrouch;

        [SerializeField] private PlayerCrouchConfig _config;

        [Header("Debug")]
        [SerializeField] private bool _isCrouching;
        private Coroutine _crouchCoroutine;

        public PlayerCrouchConfig Config => _config;
        public bool IsCrouching => _isCrouching;

        public override void Enable()
        {
            base.Enable();
            StopCrouch();
        }

        public void ToggleCrouch()
        {
            if (!_enabled)
            {
                return;
            }
            if (_isCrouching)
            {
                StopCrouch();
            }
            else
            {
                StartCrouch();
            }
        }

        private void StartCrouch()
        {
            if (_crouchCoroutine != null)
            {
                StopCoroutine(_crouchCoroutine);
            }
            _isCrouching = true;
            OnStartCrouch?.Invoke();
            _crouchCoroutine = StartCoroutine(ToggleCrouchCoroutine());
        }

        public bool StopCrouch()
        {
            if (CanUncrouch())
            {
                if (_crouchCoroutine != null)
                {
                    StopCoroutine(_crouchCoroutine);
                }
                _isCrouching = false;
                OnStopCrouch?.Invoke();
                _crouchCoroutine = StartCoroutine(ToggleCrouchCoroutine());
                return true;
            }
            return false;
        }

        private IEnumerator ToggleCrouchCoroutine()
        {
            float currentTime = 0f;
            float currentHeight = _player.CC.height;
            if (_isCrouching)
            {
                while (currentTime < _config.TimeToCrouch)
                {
                    currentTime += Time.deltaTime;
                    _player.CC.height = Mathf.Lerp(currentHeight, _config.CrouchHeight, currentTime / _config.TimeToCrouch);
                    _player.CC.center = Vector3.up * _player.CC.height / 2f;
                    _player.LookComponent.Cam.transform.parent.localPosition = Vector3.up * _player.CC.height * _config.HeadHeightPercent;
                    yield return null;
                }

            }
            else
            {
                while (currentTime < _config.TimeToStand)
                {
                    currentTime += Time.deltaTime;
                    _player.CC.height = Mathf.Lerp(currentHeight, _config.StandHeight, currentTime / _config.TimeToStand);
                    _player.CC.center = Vector3.up * _player.CC.height / 2f;
                    _player.LookComponent.Cam.transform.parent.localPosition = Vector3.up * _player.CC.height * _config.HeadHeightPercent;
                    if (UnderRoof())
                    {
                        StartCrouch();
                    }
                    yield return null;
                }
            }
            _crouchCoroutine = null;
        }

        private bool CanUncrouch()
        {
            return !UnderRoof() && !Physics.SphereCast(transform.position + Vector3.up * _config.StandHeight / 2f, _player.CC.radius, Vector3.up, out _, _config.StandHeight / 2f - _player.CC.radius * 0.95f, _config.ObstacleMask);
        }

        private bool UnderRoof()
        {
            return Physics.SphereCast(transform.position + _player.CC.center, _player.CC.radius, Vector3.up, out _, _player.CC.center.y - (_player.CC.radius * 0.8f), _config.ObstacleMask);
        }
    }
}
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerLookComponent : PlayerComponent
    {
        [SerializeField] private PlayerLookConfig _config;

        [Header("Debug")]
        [SerializeField] private Camera _cam;
        [SerializeField] private float _xRot;

        public PlayerLookConfig Config => _config;
        public Camera Cam => _cam;

        public override void Initialize()
        {
            base.Initialize();
            _cam = GetComponentInChildren<Camera>();
        }

        public override void OnLateUpdate()
        {
            if (!_enabled)
            {
                return;
            }
            base.OnLateUpdate();
            if (_player.InputComponent.LookInput.x != 0f)
            {
                transform.Rotate(Vector3.up * _player.InputComponent.LookInput.x * _config.HorizontalSpeed * (_config.InvertHorizontalInput ? -1f : 1f) * Time.deltaTime);
            }
            if (_player.InputComponent.LookInput.y != 0f)
            {
                _xRot = Mathf.Clamp(_xRot - (_player.InputComponent.LookInput.y * _config.VerticalSpeed * (_config.InvertHorizontalInput ? -1f : 1f) * Time.deltaTime), -_config.MaxVerticalAngle, _config.MinVerticalAngle);
                _cam.transform.parent.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
            }
        }
    }
}
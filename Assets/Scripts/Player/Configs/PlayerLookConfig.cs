using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Look", menuName = "Winter Universe/Player/New Look")]
    public class PlayerLookConfig : ScriptableObject
    {
        [SerializeField] private bool _invertHorizontalInput;
        [SerializeField] private bool _invertVerticalInput;
        [SerializeField] private float _horizontalSpeed = 25f;
        [SerializeField] private float _verticalSpeed = 25f;
        [SerializeField] private float _minVerticalAngle = 90f;
        [SerializeField] private float _maxVerticalAngle = 90f;
        [SerializeField] private float _interactionDistace = 1f;

        public bool InvertHorizontalInput => _invertHorizontalInput;
        public bool InvertVerticalInput => _invertVerticalInput;
        public float HorizontalSpeed => _horizontalSpeed;
        public float VerticalSpeed => _verticalSpeed;
        public float MinVerticalAngle => _minVerticalAngle;
        public float MaxVerticalAngle => _maxVerticalAngle;
        public float InteractionDistace => _interactionDistace;
    }
}
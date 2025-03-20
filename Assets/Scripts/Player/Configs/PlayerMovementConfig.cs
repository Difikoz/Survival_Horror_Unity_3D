using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Movement", menuName = "Winter Universe/Player/New Movement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField] private float _acceleration = 8f;
        [SerializeField] private float _deceleration = 16f;
        [SerializeField] private float _maxSpeed = 4f;
        [SerializeField, Range(0f, 2f)] private float _airControl = 0.25f;
        [SerializeField] private LayerMask _obstacleMask;

        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float MaxSpeed => _maxSpeed;
        public float AirControl => _airControl;
        public LayerMask ObstacleMask => _obstacleMask;
    }
}
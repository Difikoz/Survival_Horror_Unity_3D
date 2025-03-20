using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Slope", menuName = "Winter Universe/Player/New Slope")]
    public class PlayerSlopeConfig : ScriptableObject
    {
        [SerializeField, Range(30f, 60f)] private float _slopeLimit = 45f;
        [SerializeField, Range(0.1f, 4f)] private float _timeToStop = 1f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private LayerMask _obstacleMask;

        public float SlopeLimit => _slopeLimit;
        public float TimeToStop => _timeToStop;
        public float Gravity => _gravity;
        public LayerMask ObstacleMask => _obstacleMask;
    }
}
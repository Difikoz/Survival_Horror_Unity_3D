using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Slide", menuName = "Winter Universe/Player/New Slide")]
    public class PlayerSlideConfig : ScriptableObject
    {
        [SerializeField, Range(0.1f, 4f)] private float _forceMultiplier = 2f;
        [SerializeField, Range(0.1f, 2f)] private float _timeToStop = 1f;
        [SerializeField] private LayerMask _obstacleMask;

        public float ForceMultiplier => _forceMultiplier;
        public float TimeToStop => _timeToStop;
        public LayerMask ObstacleMask => _obstacleMask;
    }
}
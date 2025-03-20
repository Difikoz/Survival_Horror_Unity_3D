using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Crouch", menuName = "Winter Universe/Player/New Crouch")]
    public class PlayerCrouchConfig : ScriptableObject
    {
        [SerializeField] private float _standHeight = 1.8f;
        [SerializeField] private float _crouchHeight = 1.2f;
        [SerializeField, Range(0.5f, 0.9f)] private float _headHeightPercent = 0.9f;
        [SerializeField, Range(0.1f, 1f)] private float _timeToStand = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float _timeToCrouch = 0.5f;
        [SerializeField] private LayerMask _obstacleMask;

        public float StandHeight => _standHeight;
        public float CrouchHeight => _crouchHeight;
        public float HeadHeightPercent => _headHeightPercent;
        public float TimeToStand => _timeToStand;
        public float TimeToCrouch => _timeToCrouch;
        public LayerMask ObstacleMask => _obstacleMask;
    }
}
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Jump", menuName = "Winter Universe/Player/New Jump")]
    public class PlayerJumpConfig : ScriptableObject
    {
        [SerializeField] private float _standJumpForce = 1f;
        [SerializeField] private float _crouchJumpForce = 1.5f;
        [SerializeField, Range(1, 9)] private int _jumpCount = 1;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField, Range(0.1f, 1f)] private float _timeToJump = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float _timeToFall = 0.5f;
        [SerializeField] private LayerMask _obstacleMask;

        public float StandJumpForce => _standJumpForce;
        public float CrouchJumpForce => _crouchJumpForce;
        public int JumpCount => _jumpCount;
        public float Gravity => _gravity;
        public float TimeToJump => _timeToJump;
        public float TimeToFall => _timeToFall;
        public LayerMask ObstacleMask => _obstacleMask;
    }
}
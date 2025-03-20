using UnityEngine;

namespace WinterUniverse
{
    public abstract class PlayerComponent : MonoBehaviour
    {
        [SerializeField] protected bool _enabled = true;

        protected PlayerController _player;

        public virtual void Initialize()
        {
            _player = GetComponent<PlayerController>();
        }

        public virtual void Enable()
        {

        }

        public virtual void Disable()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void OnLateUpdate()
        {

        }
    }
}
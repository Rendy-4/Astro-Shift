using UnityEngine;

namespace AstroShift.Core
{
    public class ParticlePool : MonoBehaviour
    {
        private Animator _animator;
        private ObjectPool _pool;

        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        public void Play(ObjectPool sourcePool, Vector3 scale) {
            _pool = sourcePool;
            
            transform.localScale = scale;

            if (_animator == null || !_animator.isActiveAndEnabled) return;

            _animator.Play("Dust_effect", 0, 0f);

            float duration = _animator.GetCurrentAnimatorStateInfo(0).length;
            if (duration <= 0f) duration = 1f;
            
            CancelInvoke(nameof(ReturnToPool));
            Invoke(nameof(ReturnToPool), duration);
        }

        private void ReturnToPool() {
            _pool.ReturnToPool(this.gameObject);
        }
    }
}
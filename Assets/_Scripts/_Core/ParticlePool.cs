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

        // Tambahkan parameter scale di sini
        public void Play(ObjectPool sourcePool, Vector3 scale) {
            _pool = sourcePool;
            
            // Masalah 1: Reset/Set skala objek agar tidak "selalu besar"
            transform.localScale = scale;

            _animator.Play("DustEffect", -1, 0f);

            float duration = _animator.GetCurrentAnimatorStateInfo(0).length;
            
            // Batalkan Invoke sebelumnya jika ada (untuk keamanan)
            CancelInvoke(nameof(ReturnToPool));
            Invoke(nameof(ReturnToPool), duration);
        }

        private void ReturnToPool() {
            _pool.ReturnToPool(this.gameObject);
        }
    }
}
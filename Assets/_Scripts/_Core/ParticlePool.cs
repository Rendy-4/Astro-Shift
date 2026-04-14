using UnityEngine;

namespace AstroShift.Core
{
    public class ParticlePool : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private ObjectPool _pool;

        private void Awake() {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void Play(ObjectPool sourcePool) {
            _pool = sourcePool;
            _particleSystem.Play();

            Invoke(nameof(ReturnToPool), _particleSystem.main.duration + _particleSystem.main.startLifetime.constantMax); 
        }

        private void ReturnToPool() {
            _pool.ReturnToPool(this.gameObject);
        }
    }
}


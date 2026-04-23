using UnityEngine;
using AstroShift.Core;

namespace AstroShift.Player
{
    public class PlayerParticle : MonoBehaviour
    {
        [Header("Particle Pool Settings")]
        [SerializeField] private ObjectPool particlePool;
        [SerializeField] private Transform FeetPosition;
        [SerializeField] private float particleSpawnInterval = 0.2f;
        [SerializeField] private Vector3 runScale = new Vector3(0.5f, 0.5f, 0.5f);
        [SerializeField] private Vector3 switchScale = new Vector3(1.5f, 1.5f, 1.5f);

        private float dustTimer = 0f;

        // Dipanggil dari PlayerController setiap FixedUpdate
        public void HandleDustEffects(bool isTouchingSurface, float velocityX, bool isFlipping)
        {
            if (isTouchingSurface && Mathf.Abs(velocityX) > 0.1f)
            {
                dustTimer += Time.deltaTime;
                if (dustTimer >= particleSpawnInterval)
                {
                    SpawnDust(runScale, FeetPosition.position, !isFlipping);
                    dustTimer = 0f;
                }
            }
            else
            {
                dustTimer = 0f;
            }
        }

        // Dipanggil dari PlayerController saat SwitchGravity
        public void SpawnSwitchDust(Vector3 spawnPos, bool wasAtTop)
        {
            SpawnDust(switchScale, spawnPos, wasAtTop);
        }

        private void SpawnDust(Vector3 scale, Vector3 position, bool isAtTop)
        {
            if (particlePool == null || particlePool.gameObject == null) return;

            float yOffset = 0f;
            Vector3 spawnPosition = new Vector3(position.x, position.y + yOffset, position.z);
            Quaternion dustRotation = isAtTop ? Quaternion.Euler(180, 0, 0) : Quaternion.identity;

            GameObject dustObj = particlePool.Get(spawnPosition, dustRotation);

            if (dustObj == null) return;
            {
                ParticlePool dustParticle = dustObj.GetComponent<ParticlePool>();
                if (dustParticle != null)
                {
                    dustParticle.Play(particlePool, scale);
                }
            }
        }
    }
}
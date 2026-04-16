using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using AstroShift.Core;

namespace AstroShift.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [Header("Gravity Settings")]
        [SerializeField] private float flipDuration = 1f;
        [SerializeField] private Transform charVisual;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private LayerMask groundLayer; // Drag layer "Ground" ke sini di Inspector

        [Header("Particle Pool Settings")]
        [SerializeField] private ObjectPool particlePool;
        [SerializeField] private Transform FeetPosition;
        [SerializeField] private float particleSpawnInterval = 0.2f;
        [SerializeField] private Vector3 runScale = new Vector3(0.5f, 0.5f, 0.5f); // Skala saat lari
        [SerializeField] private Vector3 switchScale = new Vector3(1.5f, 1.5f, 1.5f); // Skala saat klik

        private Rigidbody2D rb;
        private bool isFlipping = false;
        private float dustTimer = 0f;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            isFlipping = rb.gravityScale > 0;
        }

        private void FixedUpdate() {
            HandleAutomaticMovement();
            HandleDustEffects();
        }

        private void HandleAutomaticMovement() {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }

        // Fungsi cek apakah menempel di lantai atau atap
        private bool IsTouchingSurface() {
            // Membuat box kecil di posisi kaki untuk cek tabrakan dengan ground
            return Physics2D.OverlapBox(FeetPosition.position, new Vector2(0.5f, 0.2f), 0, groundLayer);
        }

        private void HandleDustEffects()
        {
            // Hapus pengecekan velocity.y, cukup cek IsTouchingSurface dan gerak horizontal
            if (IsTouchingSurface() && Mathf.Abs(rb.linearVelocity.x) > 0.1f) 
            {
                dustTimer += Time.deltaTime;
                if (dustTimer >= particleSpawnInterval)
                {
                    // Jika isFlipping = false, berarti kita sedang di atas
                    SpawnDust(runScale, FeetPosition.position, !isFlipping); 
                    dustTimer = 0f; 
                }
            }
            else
            {
                dustTimer = 0f; 
            }
        }

        private void SpawnDust(Vector3 scale, Vector3 position, bool isAtTop)
        {
            if (particlePool == null) return;

            // JIKA PIVOT SUDAH BOTTOM: Set yOffset ke 0 agar menempel pas di kaki
            // Kamu bisa beri nilai sangat kecil (misal 0.05f) jika ingin ada sedikit celah visual
            float yOffset = 0f; 
            Vector3 spawnPosition = new Vector3(position.x, position.y + yOffset, position.z);

            // Rotasi 180 jika di atap agar pivot (yang sekarang di atas karena rotasi) tetap menempel di atap
            Quaternion dustRotation = isAtTop ? Quaternion.Euler(180, 0, 0) : Quaternion.identity;

            GameObject dustObj = particlePool.Get(spawnPosition, dustRotation);

            ParticlePool dustParticle = dustObj.GetComponent<ParticlePool>();
            if (dustParticle != null)
            {
                dustParticle.Play(particlePool, scale);
            }
        }

        public void SwitchGravity()
        {
            // 1. Simpan status LAMA sebelum ada perubahan apapun
            Vector3 spawnPos = FeetPosition.position;
            bool wasAtTop = !isFlipping; // Jika isFlipping true (di bawah), maka wasAtTop = false. BENAR.
            bool wasOnSurface = IsTouchingSurface();

            // 2. Baru setelah itu ubah status gravitasi
            rb.gravityScale *= -1;
            isFlipping = rb.gravityScale > 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            // ... (sisa kode rotasi visual karakter) ...
            float targetRotation = isFlipping ? 0f : 180f;
            float targetFlip = isFlipping ? 1f : -1f;
            float targetFeetY = isFlipping ? -0.69f : 0.83f; 

            FeetPosition.localPosition = new Vector3(FeetPosition.localPosition.x, targetFeetY, FeetPosition.localPosition.z);

            charVisual.DORotate(new Vector3(0, 0, targetRotation), flipDuration).SetEase(Ease.InOutSine).SetLink(gameObject);
            charVisual.DOScaleX(targetFlip, flipDuration).SetEase(Ease.InOutSine).SetLink(gameObject);

            // 3. Gunakan status LAMA yang sudah disimpan tadi
            if (wasOnSurface)
            {
                SpawnDust(switchScale, spawnPos, wasAtTop); 
            }
        }

        public void Die() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDrawGizmos() {
            if (FeetPosition != null) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(FeetPosition.position, new Vector2(0.5f, 0.2f));
            }
        }
    }
}
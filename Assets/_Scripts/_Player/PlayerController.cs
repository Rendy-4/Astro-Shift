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
        [SerializeField] private float flipDuration = 1f; // Skala gravitasi awal
        [SerializeField] private Transform charVisual; // Referensi ke visual karakter untuk rotasi

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 8f; // Kecepatan lari otomatis

        [Header("Particle Pool Settings")]
        [SerializeField] private ObjectPool particlePool; // Referensi ke Object Pool untuk efek partikel
        [SerializeField] private Transform FeetPosition; // Posisi di mana partikel akan muncul (misalnya di bawah kaki)
        [SerializeField] private float particleSpawnInterval = 0.2f; // Interval waktu untuk spawn partikel saat bergerak
        
        private Rigidbody2D rb;
        private bool isFlipping = false; // Menandakan apakah sedang dalam proses membalik gravitasi
        private float dustTimer = 0f; // Timer untuk mengatur spawn partikel

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Start() {
            isFlipping = rb.gravityScale > 0; // Tentukan status membalik berdasarkan gravitasi awal
        }

        private void FixedUpdate()
        {
            // PANGGIL FUNGSI GERAK DI SINI
            // FixedUpdate digunakan untuk urusan Fisika agar gerakannya halus (Smooth)
            HandleAutomaticMovement();
            HandleDustEffects();

        }

        private void HandleAutomaticMovement()
        {
            // Kita hanya mengubah kecepatan di sumbu X (Kanan)
            // Sumbu Y tetap menggunakan kecepatan asli dari Rigidbody (efek gravitasi)
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }

        private void HandleDustEffects()
        {
            // Cek jika bergerak horizontal (moveSpeed) dan menempel di lantai/atap (Y dekat 0)
            if (Mathf.Abs(rb.linearVelocity.x) > 0.1f && Mathf.Abs(rb.linearVelocity.y) < 0.1f) 
            {
                dustTimer += Time.deltaTime;
                if (dustTimer >= particleSpawnInterval)
                {
                    // Panggil fungsi SpawnDust yang sudah menyertakan logika .Play()
                    SpawnDust(); 
                    dustTimer = 0f; 
                }
            }
            else
            {
                dustTimer = 0f; 
            }
        }

        private void SpawnDust()
        {
            if (particlePool == null) return;

            GameObject dustObj = particlePool.Get(FeetPosition.position, Quaternion.identity);

            ParticlePool dustParticle = dustObj.GetComponent<ParticlePool>();
            if (dustParticle != null)
            {
                dustParticle.Play(particlePool);
            }
        }

        public void SwitchGravity()
    {
        // Daripada pakai bool manual, langsung balikkan saja nilai gravityScale yang ada
        rb.gravityScale *= -1;
    
        // Update status isFlipping berdasarkan gravityScale terbaru
        isFlipping = rb.gravityScale > 0; 
    
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        // Tentukan rotasi dan flip berdasarkan isFlipping yang sudah sinkron
        float targetRotation = isFlipping ? 0f : 180f;
        float targetFlip = isFlipping ? 1f : -1f;
    
        // Tambahkan .SetLink(gameObject) untuk menghilangkan warning DOTween yang tadi
        charVisual.DORotate(new Vector3(0, 0, targetRotation), flipDuration)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject); 
    
        charVisual.DOScaleX(targetFlip, flipDuration)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);

        SpawnDust(); // Spawn partikel saat membalik gravitasi
    }

        public void Die()
        {
            // Logika kematian, misalnya memulai ulang level atau menampilkan efek kematian
            Debug.Log("Player mati!, memulai ulang level...");
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Memulai ulang level saat player mati
        }
        
    }
}
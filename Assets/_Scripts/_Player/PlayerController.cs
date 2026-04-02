using UnityEngine;

namespace AstroShift.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 8f; // Kecepatan lari otomatis
        
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            // PANGGIL FUNGSI GERAK DI SINI
            // FixedUpdate digunakan untuk urusan Fisika agar gerakannya halus (Smooth)
            HandleAutomaticMovement();
        }

        private void HandleAutomaticMovement()
        {
            // Kita hanya mengubah kecepatan di sumbu X (Kanan)
            // Sumbu Y tetap menggunakan kecepatan asli dari Rigidbody (efek gravitasi)
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }

        public void SwitchGravity()
        {
            // Membalikkan arah gravitasi dengan mengubah nilai gravityScale
            rb.gravityScale *= -1;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset kecepatan vertikal agar tidak terlalu cepat saat membalik
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z); // Membalik skala Y untuk membalik sprite
        }

        public void Die()
        {
            // Logika kematian, misalnya memulai ulang level atau menampilkan efek kematian
            Debug.Log("Player mati!");
            // Di sini kamu bisa menambahkan logika untuk mengurangi nyawa, memulai ulang level, atau efek kematian lainnya
        }
        
    }
}
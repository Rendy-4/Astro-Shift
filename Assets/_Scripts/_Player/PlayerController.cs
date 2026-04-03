using UnityEngine;
using DG.Tweening;

namespace AstroShift.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [Header("Gravity Settings")]
        [SerializeField] private float flipDuration = 1f; // Skala gravitasi awal
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 8f; // Kecepatan lari otomatis
        
        private Rigidbody2D rb;
        private bool isFlipping = false; // Menandakan apakah sedang dalam proses membalik gravitasi

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
            isFlipping = !isFlipping; // Toggle status membalik
            rb.gravityScale = isFlipping ? 1 : -1; // Ubah arah gravitasi

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset kecepatan vertikal saat membalik

            float targetRotation = isFlipping ? 0f : 180f; // Rotasi target berdasarkan status membalik

            transform.DORotate(new Vector3(targetRotation, 0, 0), flipDuration).SetEase(Ease.InOutSine); // Animasi rotasi dengan DOTween
        }

        public void Die()
        {
            // Logika kematian, misalnya memulai ulang level atau menampilkan efek kematian
            Debug.Log("Player mati!");
            // Di sini kamu bisa menambahkan logika untuk mengurangi nyawa, memulai ulang level, atau efek kematian lainnya
        }
        
    }
}
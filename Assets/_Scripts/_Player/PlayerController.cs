using UnityEngine;

namespace AstroShift.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
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
    }
}
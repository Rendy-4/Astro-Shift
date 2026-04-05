using UnityEngine;
using TMPro;

namespace AstroShift.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("Score Settings")]
        [SerializeField] private TextMeshProUGUI scoreText; // Referensi ke UI Text untuk menampilkan skor
        [SerializeField] private Transform playerTransform; // Referensi ke transformasi player

        private float StratPosX; // Posisi awal player pada sumbu X
        private int currentScore; // Skor saat ini
        
        void Start()
        {
            if (playerTransform == null) {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Mencari player berdasarkan tag jika belum diassign
            }
            StratPosX = playerTransform.position.x;
        }

        void Update()
        {
            if (playerTransform != null) {
                // Hitung skor berdasarkan jarak yang ditempuh player dari posisi awal
                currentScore = Mathf.FloorToInt(playerTransform.position.x - StratPosX);
                scoreText.text = "Score: " + currentScore.ToString(); // Update teks skor di UI
            }
        }

        public int GetCurrentScore() {
            return currentScore; // Mengembalikan skor saat ini
        }
    }
}


using UnityEngine;

namespace AstroShift.Manager
{
    public class ObstacleManager : MonoBehaviour
    {
        [Header("Obstacle Settings")]
        [SerializeField] private GameObject obstaclePrefabs; // Array untuk menyimpan prefab rintangan
        [SerializeField] private float spawnInterval = 2f; // Interval waktu untuk spawn rintangan
        [SerializeField] private float spawnOffset = 5f; // Jarak horizontal dari player untuk spawn rintangan

        [Header("Spawn Position Settings")]
        [SerializeField] private float minY = -3f; // Batas bawah untuk posisi spawn rintangan
        [SerializeField] private float maxY = 3f; // Batas atas untuk

        private float timer = 0f; // Timer untuk mengatur interval spawn
        private Transform playerTransform; // Referensi ke transformasi player

        private void Start() {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Mencari player berdasarkan tag
        }

        private void Update() {
            timer += Time.deltaTime; // Menambahkan waktu yang telah berlalu ke timer
            if (timer >= spawnInterval) {
                SpawnObstacle(); // Memanggil fungsi untuk spawn rintangan
                timer = 0f; // Reset timer setelah spawn
            }
        }

        private void SpawnObstacle() {
            // Tentukan posisi muncul: X di depan player, Y acak antara lantai/atap
            float randomY = Random.Range(0, 2) == 0 ? minY : maxY; 
            Vector3 spawnPos = new Vector3(playerTransform.position.x + spawnOffset, randomY, 0);

            // Munculkan rintangan
            Instantiate(obstaclePrefabs, spawnPos, Quaternion.identity);
        }
    }
}


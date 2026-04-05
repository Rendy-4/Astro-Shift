using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace AstroShift.Manager
{
    public class ProgressManager : MonoBehaviour
    {
        [Header("Progress Settings")]
        [SerializeField] private TextMeshProUGUI progressText; // Referensi ke UI Text untuk menampilkan progress
        [SerializeField] private Transform playerTransform; // Referensi ke transformasi player
        [SerializeField] private Image progressBar; // Referensi ke UI Image untuk menampilkan progress bar
        
        [Header("Level Settings")]
        [SerializeField] private float levelLength = 100f; // Panjang level untuk menghitung progress

        private float startPosX; // Posisi awal player pada sumbu X

        void Start()
        {
            if (playerTransform == null) {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Mencari player berdasarkan tag jika belum diassign
            }
            startPosX = playerTransform.position.x;
        }

        // Update is called once per frame
        void Update()
        {
            if (playerTransform != null) {
                // Hitung progress berdasarkan jarak yang ditempuh player dari posisi awal dibandingkan dengan panjang level
                float distanceTraveled = playerTransform.position.x - startPosX;
                float progress = Mathf.Clamp01(distanceTraveled / levelLength); // Pastikan progress antara 0 dan 1

                // Update teks progress di UI
                progressText.text = Mathf.FloorToInt(progress * 100) + "%";
                
                // Update fill amount pada progress bar
                if (progressBar != null) {
                    progressBar.fillAmount = progress; // Set fill amount sesuai dengan progress
                }

                if (progressBar != null) {
                    progressBar.fillAmount = progress;
                    // Hapus baris ini setelah tes berhasil
                    Debug.Log("Progress saat ini: " + progress); 
                }
            }

            
        }
    }
}


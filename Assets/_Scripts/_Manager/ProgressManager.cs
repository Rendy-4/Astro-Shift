using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AstroShift.Player;

namespace AstroShift.Manager
{
    public class ProgressManager : MonoBehaviour
    {
        [Header("Progress Settings")]
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Image progressBar;
        
        [Header("Level Settings")]
        [SerializeField] private GameObject levelEndPoint;
        private float levelLength;
        private float startPosX;
        private bool isFinished = false; // TAMBAHKAN INI agar tidak error di Update

        [Header("End Screen")]
        [SerializeField] private GameObject endScreen;

        void Start()
        {
            if (playerTransform == null) {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
            startPosX = playerTransform.position.x;

            // Hitung levelLength di Start agar tidak 0
            if (levelEndPoint != null)
            {
                levelLength = levelEndPoint.transform.position.x - startPosX;
            }
        }

        void Update()
        {
            if (playerTransform != null && !isFinished && levelLength > 0)
            {
                float progress = Mathf.Clamp01((playerTransform.position.x - startPosX) / levelLength);
                progressText.text = $"{(progress * 100f):0}%";

                if (progressBar != null)
                {
                    progressBar.fillAmount = progress;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                FinishLevel();
            }
        }

        // PERBAIKAN: Tambahkan kata 'void' di sini
        private void FinishLevel() 
        {
            isFinished = true;
            Debug.Log("Level Completed!");

            if (endScreen != null)
            {
                endScreen.SetActive(true);
            }
            Time.timeScale = 0f; 
        }
    }
}
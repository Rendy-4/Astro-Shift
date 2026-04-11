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

        [Header("UI Settings")]
        [SerializeField] private float smoothTime = 0.3f;

        
        private float currentDisplayProgress = 0f;

        void Start()
        {
            Time.timeScale = 1f; // Pastikan waktu berjalan normal saat level dimulai
            if (playerTransform == null) {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj != null) {
                    playerTransform = playerObj.transform;
                }
            }

            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            if (spawnPoint != null && playerTransform !=null)
            {
                playerTransform.position = spawnPoint.transform.position;
                Debug.Log("Player teleported to spawn point at: " + spawnPoint.transform.position);
            }

            if (playerTransform != null && levelEndPoint != null)
            {
                startPosX = playerTransform.position.x;
                levelLength = levelEndPoint.transform.position.x - startPosX;
            }
        }

        void Update()
        {
            if (playerTransform != null && !isFinished && levelLength > 0)
            {
                float progress = Mathf.Clamp01((playerTransform.position.x - startPosX) / levelLength);
                currentDisplayProgress = Mathf.MoveTowards(currentDisplayProgress, progress,smoothTime * Time.deltaTime);
                progressText.text = $"{(currentDisplayProgress * 100f):0}%";

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

        private void FinishLevel() 
        {
            if (isFinished) return; // Cegah pemanggilan ganda
            isFinished = true;
            currentDisplayProgress = 1f;
            Debug.Log("Level Completed!");

            if (progressText != null) progressText.text = "100%";
            if (progressBar != null) progressBar.fillAmount = 1f;
            if (endScreen != null) endScreen.SetActive(true);
            Time.timeScale = 0f; 
        }
    }
}
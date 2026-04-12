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

        [Header("Player Settings")]
        [SerializeField] private GameObject playerPrefab;

        
        private float currentDisplayProgress = 0f;

        void Start()
        {
            Time.timeScale = 1f;

            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            
            // Cari player yang sudah ada di scene dulu
            if (playerTransform == null)
            {
                GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
                if (existingPlayer != null)
                    playerTransform = existingPlayer.transform;
            }

            // Kalau masih null, spawn dari prefab
            if (playerTransform == null && playerPrefab != null && spawnPoint != null)
            {
                GameObject playerObj = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
                playerTransform = playerObj.transform; // ← Assign transform-nya!
            }

            // Teleport ke spawn point
            if (spawnPoint != null && playerTransform != null)
            {
                Rigidbody2D rb = playerTransform.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.position = spawnPoint.transform.position;
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
                playerTransform.position = spawnPoint.transform.position;
                startPosX = playerTransform.position.x;
                Debug.Log("Player at: " + playerTransform.position);
            }

            if (levelEndPoint != null)
                levelLength = levelEndPoint.transform.position.x - startPosX;
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
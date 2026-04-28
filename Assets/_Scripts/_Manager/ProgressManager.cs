using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AstroShift.Player;
using AstroShift.Manager;
using System.Collections;

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
        private bool isFinished = false;

        [Header("End Screen")]
        [SerializeField] private GameObject endScreen;

        [Header("UI Settings")]
        [SerializeField] private float smoothTime = 5f;

        [Header("Player Settings")]
        [SerializeField] private GameObject playerPrefab;

        
        private float currentDisplayProgress = 0f;

        void Start()
        {
            Time.timeScale = 1f;

            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            
            if (playerTransform == null)
            {
                GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
                if (existingPlayer != null)
                    playerTransform = existingPlayer.transform;
            }

            if (playerTransform == null && playerPrefab != null && spawnPoint != null)
            {
                GameObject playerObj = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
                playerTransform = playerObj.transform;
            }

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
            if (isFinished) return;
            isFinished = true;
            currentDisplayProgress = 1f;

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

            if (currentSceneIndex >= unlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", currentSceneIndex + 1);
                PlayerPrefs.Save();
            }
            

            if (progressText != null) progressText.text = "100%";
            if (progressBar != null) progressBar.fillAmount = 1f;

            StartCoroutine(FinishAfterSfx());
        }

        private IEnumerator FinishAfterSfx()
        {
            PlayerController player = Object.FindFirstObjectByType<PlayerController>();
            if (player != null) player.ManualDisable();

            if (SettingManager.Instance != null)
            {
                SettingManager.Instance.PlayInPortalSfx();
            }

            yield return new WaitForSecondsRealtime(0.35f);

            if (EndScreenManager.Instance != null)
            {
                EndScreenManager.Instance.ShowEndScreen();
            }

            Time.timeScale = 0f;
        }
    }
}
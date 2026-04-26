using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using AstroShift.Player;

namespace AstroShift.Manager
{
    public class EndScreenManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI attemptsText;
        [SerializeField] private TextMeshProUGUI clicksText;
        [SerializeField] private GameObject endScreeenPanel;
        [SerializeField] private GameObject PausePanel;

        private bool openSelectLevel = false;
        public static event System.Action OnMainMenuLoaded;
        private static bool isLevelSelectOpen = false;

        public static EndScreenManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main Menu" && openSelectLevel)
            {
                openSelectLevel = false;
                isLevelSelectOpen = true;
                Debug.Log("isLevelSelectOpen: true");
                OnMainMenuLoaded?.Invoke();
            }
        }

        public void SetUIReference(TextMeshProUGUI attempts, TextMeshProUGUI clicks, GameObject panel, GameObject pausePanel)
        {
            attemptsText = attempts;
            clicksText = clicks;
            endScreeenPanel = panel;
            PausePanel = pausePanel;
        }

        public void ShowEndScreen()
        {
            Debug.Log("ShowEndScreen dipanggil!");
            if (attemptsText != null && clicksText != null)
            {
                attemptsText.text = "ATTEMPTS: " + LevelStastManager.attemptCount.ToString();
                clicksText.text = "CLICKS: " + LevelStastManager.clickCount.ToString();
                Debug.Log($"Update UI: Attempts={LevelStastManager.attemptCount}, Clicks={LevelStastManager.clickCount}");
            }
            else 
            {
                Debug.LogError("Referensi UI (Text) di EndScreenManager masih KOSONG/NULL!");
            }

            if (endScreeenPanel != null) endScreeenPanel.SetActive(true);
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            PlayerController player = Object.FindFirstObjectByType<PlayerController>();
            if (player != null)
            {
                player.ManualDisable();
            }
            
            if (SettingManager.Instance != null) SettingManager.Instance.RestartMusic();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LevelSelect()
        {
            Time.timeScale = 1f;
            openSelectLevel = true;
            Debug.Log("LevelSelect() dipanggil!, openSelectLevel: true");
            SceneManager.LoadScene("Main Menu");
        }

        public bool IsLevelSelectOpen()
        {
            if (isLevelSelectOpen)
            {
                isLevelSelectOpen = false;
                return true;
            }
            return false;
        }

        public bool IsLastLevel()
        {
            return SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1;
        }

        public void NextLevel()
        {
            LevelStastManager.ResetStats();

            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(nextSceneIndex);
                LevelStastManager.ResetStats();
            }
            else
            {
                Debug.Log("No more levels available!");
            }
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            if (SettingManager.Instance != null) SettingManager.Instance.PauseMusic();
            PausePanel.SetActive(true);
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            if (SettingManager.Instance != null) SettingManager.Instance.UnpauseMusic();
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
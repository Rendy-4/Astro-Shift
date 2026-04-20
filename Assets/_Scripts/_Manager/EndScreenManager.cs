using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace AstroShift.Manager
{
    public class EndScreenManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI attemptsText;
        [SerializeField] private TextMeshProUGUI clicksText;
        [SerializeField] private GameObject endScreeenPanel;

        private bool openSelectLevel = false;
        public static event System.Action OnMainMenuLoaded;

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
                OnMainMenuLoaded?.Invoke();
            }
        }

        public void SetUIReference(TextMeshProUGUI attempts, TextMeshProUGUI clicks, GameObject panel)
        {
            attemptsText = attempts;
            clicksText = clicks;
            endScreeenPanel = panel;
        }

        public void ShowEndScreen()
        {
            if (attemptsText != null && clicksText != null)
            {
                attemptsText.text = "ATTEMPTS: " + LevelStastManager.attemptCount.ToString();
                clicksText.text = "CLICKS: " + LevelStastManager.clickCount.ToString();
            }

            if (endScreeenPanel != null) endScreeenPanel.SetActive(true);
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LevelSelect()
        {
            Time.timeScale = 1f;
            openSelectLevel = true;
            SceneManager.LoadScene("Main Menu");
        }

        public bool IsLevelSelectOpen()
        {
            if (openSelectLevel)
            {
                openSelectLevel = false;
                return true;
            }
            return false;
        }

        public void NextLevel()
        {
            LevelStastManager.ResetStats();

            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("No more levels available!");
            }
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
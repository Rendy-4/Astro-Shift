using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AstroShift.Manager
{
    public class EndScreenManager : MonoBehaviour
    {
        private bool openSelectLevel = false;
        public static event System.Action OnMainMenuLoaded;

        public static EndScreenManager Instance { get; private set; }
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
        public void Restart()
        {
            Time.timeScale = 1f; // Pastikan waktu berjalan normal saat level dimulai
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LevelSelect()
        {
            Time.timeScale = 1f;
            openSelectLevel = true;
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe untuk mendeteksi saat Main Menu dimuat
            SceneManager.LoadScene("Main Menu");
        }

        public bool IsLevelSelectOpen()
        {
            if (openSelectLevel) {
                openSelectLevel = false; // Reset flag setelah digunakan
                return true;
            }
            return false;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe untuk mencegah pemanggilan ganda jika scene lain dimuat
            if (scene.name == "Main Menu" && openSelectLevel)
            {
                openSelectLevel = false;

                OnMainMenuLoaded?.Invoke(); // Panggil event untuk memberi tahu bahwa Main Menu telah dimuat
            }
        }

        public void NextLevel()
        {
            
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                Time.timeScale = 1f; // Pastikan waktu berjalan normal saat level dimulai
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



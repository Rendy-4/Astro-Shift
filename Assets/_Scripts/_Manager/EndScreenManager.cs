using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AstroShift.Manager
{
    public class EndScreenManager : MonoBehaviour
    {
        public static EndScreenManager Instance { get; private set; }
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
        [SerializeField] private GameObject levelSelect; // Referensi ke Level Select untuk ditampilkan saat kembali ke Main Menu
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f; // Pastikan waktu berjalan normal saat level dimulai
        }

        public void LevelSelect()
        {
            Time.timeScale = 1f;
            // Kembali ke Main Menu lalu menyalakan object Level Select
            SceneManager.LoadScene("Main Menu");

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe untuk mencegah pemanggilan ganda jika scene lain dimuat
            if (scene.name == "Main Menu" && levelSelect != null)
            {
                levelSelect.SetActive(true);
                SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe setelah digunakan
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



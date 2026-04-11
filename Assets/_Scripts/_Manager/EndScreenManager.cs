using UnityEngine;
using UnityEngine.SceneManagement;

namespace AstroShift.Manager
{
    public class EndScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelSelect; // Referensi ke Level Select untuk ditampilkan saat kembali ke Main Menu
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LevelSelect()
        {
            // Kembali ke Main Menu lalu menyalakan object Level Select
            SceneManager.LoadScene("Main Menu");
            if (levelSelect != null)     
            {
                levelSelect.SetActive(true);
            }
        }
    }
}



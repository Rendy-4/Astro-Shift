using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AstroShift.Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Button[] levelButtons;

        private void Awake() {
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (i + 1 > unlockedLevel)
                {
                    levelButtons[i].interactable = false;
                }
                else
                {
                    levelButtons[i].interactable = true;
                }
            }
        }
        public void OpenLevel(int Level_ID)
        {
            string sceneName = "Level " + Level_ID;
            SceneManager.LoadScene(sceneName);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace AstroShift.Manager
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void SetTitleText(string text)
        {
            if (titleText != null)
            {
                titleText.text = text;
            }
        }
    }
}

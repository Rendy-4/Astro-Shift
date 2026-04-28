using UnityEngine;
using UnityEngine.UI;
using AstroShift.Manager;

namespace AstroShift.Core
{
    public class ButtonBinder : MonoBehaviour
    {
        public enum ButtonType
        {
            Restart,
            LevelSelect,
            NextLevel,
            Pause,
            Resume
        }
        [SerializeField] private ButtonType buttonType; 

        void Start()
        {
            Button button = GetComponent<Button>();

            button.onClick.RemoveAllListeners();

            if (buttonType == ButtonType.Restart)
            {
                button.onClick.AddListener(() => EndScreenManager.Instance.Restart());
            }
            else if (buttonType == ButtonType.LevelSelect)
            {
                button.onClick.AddListener(() =>{
                    if (SettingManager.Instance != null) {
                        SettingManager.Instance.PlayClickOutSfx();
                    }
                });
                button.onClick.AddListener(() => EndScreenManager.Instance.LevelSelect());
            }
            else if (buttonType == ButtonType.NextLevel)
            {
                if (EndScreenManager.Instance.IsLastLevel())
                {
                    gameObject.SetActive(false);
                    return;
                }
                button.onClick.AddListener(() => EndScreenManager.Instance.NextLevel());
            }
            else if (buttonType == ButtonType.Pause)
            {
                button.onClick.AddListener(() => EndScreenManager.Instance.Pause());
            }
            else if (buttonType == ButtonType.Resume)
            {
                button.onClick.AddListener(() => EndScreenManager.Instance.Resume());
            }
        }
    }
}


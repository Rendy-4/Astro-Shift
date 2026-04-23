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
        [SerializeField] private ButtonType buttonType; // Pilih jenis tombol di Inspector
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Button button = GetComponent<Button>();

            button.onClick.RemoveAllListeners(); // Hapus semua listener yang mungkin sudah ada untuk mencegah pemanggilan ganda

            if (buttonType == ButtonType.Restart)
            {
                button.onClick.AddListener(() => EndScreenManager.Instance.Restart());
            }
            else if (buttonType == ButtonType.LevelSelect)
            {
                button.onClick.AddListener(() =>{
                    if (AudioManager.Instance != null) {
                        AudioManager.Instance.PlayClickOutSfx();
                    }
                });
                button.onClick.AddListener(() => EndScreenManager.Instance.LevelSelect());
            }
            else if (buttonType == ButtonType.NextLevel)
            {
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


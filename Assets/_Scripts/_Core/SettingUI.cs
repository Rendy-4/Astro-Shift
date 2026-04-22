using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AstroShift.Manager;

namespace AstroShift.Core
{
    public class SettingUI : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void OnEnable() {
            if (AudioManager.Instance == null)
            {
                Debug.Log("AudioManager.Instance is null");
                return;
            }
            
            musicSlider.value = AudioManager.Instance.GetMusicVolume();
            sfxSlider.value = AudioManager.Instance.GetSFXVolume();

            musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }

        private void OnDisable() {
            musicSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.RemoveAllListeners();
        }
    }
}

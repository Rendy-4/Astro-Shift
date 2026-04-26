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
            if (SettingManager.Instance == null)
            {
                Debug.Log("AudioManager.Instance is null");
                return;
            }
            
            musicSlider.value = SettingManager.Instance.GetMusicVolume();
            sfxSlider.value = SettingManager.Instance.GetSFXVolume();

            musicSlider.onValueChanged.AddListener(SettingManager.Instance.SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SettingManager.Instance.SetSFXVolume);
        }

        private void OnDisable() {
            musicSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.RemoveAllListeners();
        }
    }
}

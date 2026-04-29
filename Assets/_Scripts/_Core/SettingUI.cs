using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AstroShift.Manager;
using System.Collections;

namespace AstroShift.Core
{
    public class SettingUI : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void OnEnable() {
            if (SettingManager.Instance == null)
            {
                StartCoroutine(WaitAndSetup());
                return;
            }
            SetUp();
        }

        private IEnumerator WaitAndSetup()
        {
            yield return null;
            if (SettingManager.Instance != null) SetUp();
        }

        private void SetUp()
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.RemoveAllListeners();
            
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

using UnityEngine;
using UnityEngine.Audio;

namespace AstroShift.Manager
{
    public class SettingManager : MonoBehaviour
    {
        public static SettingManager Instance { get; private set; }

        [Header("Audio Mixers")]
        [SerializeField] private AudioMixer audioMixer;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("SFX Clips")]
        [SerializeField] private AudioClip clickInSfx;
        [SerializeField] private AudioClip clickOutSfx;
        [SerializeField] private AudioClip deathSfx;
        [SerializeField] private AudioClip inPortalSfx;
        [SerializeField] private AudioClip oxygenSfx;
        [SerializeField] private AudioClip shieldSfx;
        [SerializeField] private AudioClip speedSfx;

        private const string MUSIC_KEY = "MusicVolume";
        private const string SFX_KEY = "SFXVolume";

        private void Awake() {
            if (Instance == null) 
            {
                Instance = this;
                
                // Auto-assign dari children
                AudioSource[] sources = GetComponentsInChildren<AudioSource>(true);
                foreach (var src in sources) {
                    src.gameObject.SetActive(true); // Pastikan aktif untuk deteksi
                    src.enabled = true; // Pastikan komponen aktif

                    string objName = src.gameObject.name.ToLower();
                    if (objName == "music") musicSource = src;
                    if (objName == "sfx") sfxSource = src;
                }
            } 
            else 
            {
                Destroy(gameObject);
            }
            LoadSavedVolumes();
        }

        private void LoadSavedVolumes()
        {
            SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_KEY, 1f));
            SetSFXVolume(PlayerPrefs.GetFloat(SFX_KEY, 1f));
        }

        public void SetMusicVolume(float value)
        {
            float db = value > 0.001f ? Mathf.Log10(value) * 20f : -80f;
            audioMixer.SetFloat("MusicVolume", db);

            PlayerPrefs.SetFloat(MUSIC_KEY, value);
            PlayerPrefs.Save();
        }
        public float GetMusicVolume() => PlayerPrefs.GetFloat(MUSIC_KEY, 0.5f);

        public void SetSFXVolume(float value)
        {
            float db = value > 0.001f ? Mathf.Log10(value) * 20f : -80f;
            audioMixer.SetFloat("SFXVolume", db);

            PlayerPrefs.SetFloat(SFX_KEY, value);
            PlayerPrefs.Save();
        }
        public float GetSFXVolume() => PlayerPrefs.GetFloat(SFX_KEY, 0.5f);

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (musicSource.clip == clip) return; // Cek jika musik yang sama sudah diputar
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }

        public void RestartMusic()
        {
            musicSource.Stop();
            musicSource.Play();
        }
        public void PauseMusic() => musicSource.Pause();
        public void UnpauseMusic() => musicSource.UnPause();

        public void playSfx(AudioClip clip)
        {
            if (clip != null)
            {
                
                sfxSource.PlayOneShot(clip);
            }
        }

        public void PlayClickInSfx() => playSfx(clickInSfx);
        public void PlayClickOutSfx() => playSfx(clickOutSfx);
        public void PlayDeathSfx() => playSfx(deathSfx);
        public void PlayInPortalSfx() => playSfx(inPortalSfx);
        public void PlayOxygenSfx() => playSfx(oxygenSfx);
        public void PlayShieldSfx() => playSfx(shieldSfx);
        public void PlaySpeedSfx() => playSfx(speedSfx);
    }
}
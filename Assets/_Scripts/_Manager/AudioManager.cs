using UnityEngine;

namespace AstroShift.Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Button SFX Clips")]
        [SerializeField] private AudioClip clickInSfx;
        [SerializeField] private AudioClip clickOutSfx;

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
        }
        
        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (musicSource.clip == clip) return; // Cek jika musik yang sama sudah diputar
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }

        public void playSfx(AudioClip clip)
        {
            if (clip != null)
            {
                
                sfxSource.PlayOneShot(clip);
            }
        }

        public void PlayClickInSfx() 
        {
            playSfx(clickInSfx);
        }
        public void PlayClickOutSfx() => playSfx(clickOutSfx);
    }
}
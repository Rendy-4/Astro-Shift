using UnityEngine;
using AstroShift.Manager;

namespace AstroShift.Enviroment
{
    public class LevelMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip levelMusicClip;

        private void Start() {
            if (AudioManager.Instance != null) {
                AudioManager.Instance.PlayMusic(levelMusicClip);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using AstroShift.Manager;

namespace AstroShift.Core {
    public class BackManager : MonoBehaviour
    {
        
        private void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.name != "Main Menu") return;
            StartCoroutine(CheckAndOpenLevelPanel());
        }

        private IEnumerator CheckAndOpenLevelPanel() {
            yield return null; // Tunggu 1 frame agar EndScreenManager selesai set flag

            if (EndScreenManager.Instance == null) yield break;
            if (!EndScreenManager.Instance.IsLevelSelectOpen()) yield break;

            GameObject canvas = GameObject.Find("Canvas Main Menu");
            if (canvas == null)
            {
                Debug.Log("Canvas Main Menu not found!");
                yield break;
            }
            
            Transform levelPanel = canvas.transform.Find("Level_Panel");
            if (levelPanel != null)
            {
                levelPanel.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Level_Panel not found!");
            }
        }
    }    
}

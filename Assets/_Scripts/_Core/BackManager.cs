using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using AstroShift.Manager;

namespace AstroShift.Core {
    public class BackManager : MonoBehaviour
    {
        // Hapus [SerializeField] - referensi ini tidak bisa diandalkan lintas scene
        
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

            bool shouldOpen = EndScreenManager.Instance.IsLevelSelectOpen();
            Debug.Log($"Apakah harus buka Level Select? {shouldOpen}");

            if (!shouldOpen) yield break;

            // Selalu cari fresh — jangan pakai cache referensi lintas scene
            GameObject[] allObjects = FindObjectsByType<GameObject>(
                FindObjectsInactive.Include, FindObjectsSortMode.None);
                
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "Level_Panel" && obj.scene.name == "Main Menu")
                {
                    obj.SetActive(true);
                    Debug.Log("Level_Panel berhasil diaktifkan!");
                    yield break;
                }
            }
            
            Debug.LogError("Gagal menemukan Level_Panel! Cek nama di Hierarchy.");
        }
    }    
}

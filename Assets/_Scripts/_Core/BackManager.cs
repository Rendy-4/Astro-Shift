using UnityEngine;
using AstroShift.Manager;

public class BackManager : MonoBehaviour
{
    [SerializeField] private GameObject levelSelect; // Referensi ke Level Select untuk ditampilkan saat kembali ke Main Menu

    private void Start() {
        if (EndScreenManager.Instance == null) return;

        bool shouldOpen = EndScreenManager.Instance.IsLevelSelectOpen();
        Debug.Log($"EndScreenManager.Instance: {EndScreenManager.Instance}");
        Debug.Log($"IsLevelSelectOpen: {EndScreenManager.Instance?.IsLevelSelectOpen()}");

        if (shouldOpen) {
            HandleMainMenuLoaded();
        }
    }

    private void HandleMainMenuLoaded() {
        if (levelSelect == null)
        {
            levelSelect = GameObject.Find("Level_Panel");
        }
        if (levelSelect != null) {
            levelSelect.SetActive(true); // Tampilkan panel Level Select
        } else {
            Debug.LogWarning("Level_Panel not found in the Main Menu scene.");
        }
    }
}

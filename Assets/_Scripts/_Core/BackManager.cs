using UnityEngine;
using AstroShift.Manager;

public class BackManager : MonoBehaviour
{
    [SerializeField] private GameObject levelSelect; // Referensi ke Level Select untuk ditampilkan saat kembali ke Main Menu

    private void OnEnable() {
        EndScreenManager.OnMainMenuLoaded += HandleMainMenuLoaded;
    }

    private void OnDisable() {
        EndScreenManager.OnMainMenuLoaded -= HandleMainMenuLoaded;
    }

    private void HandleMainMenuLoaded() {
        if (levelSelect != null) {
            levelSelect.SetActive(true); // Tampilkan panel Level Select
        } else {
            Debug.LogWarning("Level_Panel not found in the Main Menu scene.");
        }
    }
}

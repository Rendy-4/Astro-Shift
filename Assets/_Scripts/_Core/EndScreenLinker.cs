using UnityEngine;
using TMPro;
using AstroShift.Manager;

namespace AstroShift.Core
{
    public class EndScreenLinker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI attemptsText;
        [SerializeField] private TextMeshProUGUI clicksText;
        [SerializeField] private GameObject endScreeenPanel;
        [SerializeField] private GameObject PausePanel;

        
        private void Start() {
            if (EndScreenManager.Instance != null) {
                EndScreenManager.Instance.SetUIReference(attemptsText, clicksText, endScreeenPanel, PausePanel);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AstroShift.Manager;
using System.Collections;

namespace AstroShift.Core
{
    public class HintUi : MonoBehaviour
    {
        [Header("Reference UI")]
        [SerializeField] private GameObject hintPanel;
        [SerializeField] private Image mouseImage;
        [SerializeField] private Image keyImage;
        [SerializeField] private TextMeshProUGUI hintText;

        [Header("Animation Settings")]
        [SerializeField] private float pulseSpeed = 1f;
        [SerializeField] private float pulseMin = 0.5f;
        [SerializeField] private float pulseMax = 1.5f;

        private CanvasGroup canvasGroup;
        private float time;
        private bool isPulsing;

        private void Start() {
            canvasGroup = hintPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = hintPanel.GetComponent<CanvasGroup>();
            }

            InputManager.OnGravityInput += OnGravityPressed;
        }

        void OnDestroy()
        {
            InputManager.OnGravityInput -= OnGravityPressed;
        }

        private void Update() {
            if (isPulsing)
            {
                time += Time.deltaTime;
                float alpha = Mathf.Lerp(pulseMin, pulseMax, (Mathf.Sin(time) + 1) / 2);
                canvasGroup.alpha = alpha;
            }
        }

        public void ShowHint(bool show)
        {
            hintPanel.SetActive(show);
        }

        public void OnGravityPressed ()
        {
            isPulsing = false;
            canvasGroup.alpha = 1f;
            time = 0f;

            StartCoroutine(FadeOutHint());
        }

        private IEnumerator FadeOutHint ()
        {
            float duration = 0.5f;
            float elapsed = 0f;
            float startAlpha = canvasGroup.alpha;

            while (elapsed < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 0f;
            hintPanel.SetActive(false);
        }
    }    
}
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace AstroShift.Core
{
    public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private float hoverScale = 1.1f;
        [SerializeField] private float hoverDuration = 0.2f;
        
        private Vector3 originalScale;

        private void Awake() {
            originalScale = transform.localScale;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(originalScale * hoverScale, hoverDuration)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(originalScale, hoverDuration)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            transform.DOKill();
            transform.DOScale(originalScale * 0.9f, 0.05f)
                .SetUpdate(true)
                .OnComplete(() =>
                    transform.DOScale(originalScale, 0.05f)
                        .SetUpdate(true)
                );
        }

        private void OnDisable()
        {
            transform.DOKill();
            transform.localScale = originalScale;
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }    
}
using UnityEngine;
using DG.Tweening;

namespace AstroShift.Core
{
    public class PanelAnim : MonoBehaviour
    {
        public enum AnimType {fromTop, ScaleUP}

        [SerializeField] private AnimType animType;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float offsetY = 800f;

        private Vector2 originalPos;
        private Vector3 originalScale;
        private RectTransform rect;

        private void Awake() {
            rect = GetComponent<RectTransform>();
            originalPos = rect.anchoredPosition;
            originalScale = rect.localScale;
        }

        private void OnEnable()
        {
            if (animType == AnimType.fromTop)
            {
                rect.anchoredPosition = new Vector2(originalPos.x, originalPos.y + offsetY);
                rect.DOAnchorPos(originalPos, duration)
                    .SetEase(Ease.OutBounce)
                    .SetUpdate(true);
                    
            }
            else if (animType == AnimType.ScaleUP)
            {
                rect.localScale = Vector3.zero;
                rect.DOScale(originalScale, duration)
                    .SetEase(Ease.OutBack)
                    .SetUpdate(true);
                
            }
        }

        private void OnDisable()
        {
            rect.DOKill();
        }

        private void OnDestroy()
        {
            rect.DOKill();
        }
    }    
}
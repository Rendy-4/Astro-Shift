using UnityEngine;

namespace AstroShift.Environment
{
    [System.Serializable]
    public class BackgroundElement
    {
        [SerializeField] private SpriteRenderer backgroundSprite;
        [Range(0f, 1f)] public float scrollSpeed;
        
        [HideInInspector] public Material spriteMaterial;
        public SpriteRenderer BackgroundSprite => backgroundSprite;
    }

    public class ParallaxBackground : MonoBehaviour
    {
        private const float SCROLL_MULTIPLIER = 0.01f;
        [SerializeField] private BackgroundElement[] backgroundElements;

        private void Start()
        {
            foreach (BackgroundElement element in backgroundElements)
            {
                if (element.BackgroundSprite != null)
                {
                    // Pastikan material di-assign di awal
                    element.spriteMaterial = element.BackgroundSprite.material;
                }
            }
        }

        private void Update()
        {
            foreach (BackgroundElement element in backgroundElements)
            {
                if (element.spriteMaterial != null)
                {
                    // PERBAIKAN: Gunakan '=' dan transform.position.x 
                    // Bukan '+=' agar sinkron dengan gerak kamera
                    float offset = transform.position.x * element.scrollSpeed * SCROLL_MULTIPLIER;
                    element.spriteMaterial.mainTextureOffset = new Vector2(offset, 0f);
                }
            }
        }
    }
}
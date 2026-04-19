using UnityEngine;
using System.Collections;

namespace AstroShift.Player
{
    public class PlayerShield : MonoBehaviour
    {
        public bool IsActive { get; private set; }
        [SerializeField] private GameObject shieldVisual;

        private void Awake() {
            if (shieldVisual != null) 
            {
                shieldVisual.SetActive(false);
            }
        }

        public void ActiveShield (float duration)
        {
            StopAllCoroutines();
            StartCoroutine(ShieldDuration(duration));
        }

        public void BreakShield()
        {
            StopAllCoroutines();
            IsActive = false;
            if (shieldVisual != null) 
            {
                shieldVisual.SetActive(false);
            }
        }

        private IEnumerator ShieldDuration(float duration)
        {
            IsActive = true;
            if (shieldVisual != null) 
            {
                shieldVisual.SetActive(true);
            }

            yield return new WaitForSeconds(duration);

            IsActive = false;
            if (shieldVisual != null) 
            {
                shieldVisual.SetActive(false);
            }
        }
    }
}
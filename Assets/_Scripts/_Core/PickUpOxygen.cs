using UnityEngine;
using AstroShift.Manager;

namespace AstroShift.Core
{
    public class PickUpOxygen : MonoBehaviour
    {
        [SerializeField] private float oxygenAmount = 30f; // Berapa banyak oksigen yang ditambah

            private void OnTriggerEnter2D(Collider2D other)
            {
                // Pastikan yang menyentuh adalah player
                if (other.CompareTag("Player"))
                {
                    // Isi oksigen player
                    OxygenManager.Instance.RefillOxygen(oxygenAmount);

                    // Hancurkan item setelah diambil
                    Destroy(gameObject);
                }
            }
    }
}

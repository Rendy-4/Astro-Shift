using UnityEngine;
using AstroShift.Manager;
using AstroShift.Player;

namespace AstroShift.Core
{
    public class PickUpItem : MonoBehaviour
    {
        public enum ItemType
        {
            Oxygen,
            Shield,
            SpeedBoost
        }

        [Header("Item Settings")]
        [SerializeField] private ItemType itemType; // Jenis item yang bisa dipilih
        [SerializeField] private float Amount = 30f; // Berapa banyak oksigen yang ditambah
        [SerializeField] private float Duration = 5f; // Durasi perisai
        [SerializeField] private float multiplier = 2f; // Multiplier untuk speed boost

        private PlayerShield playerShield;
        private PlayerController playerController;

            private void OnTriggerEnter2D(Collider2D other)
            {
                // Pastikan yang menyentuh adalah player
                if (other.CompareTag("Player"))
                {
                    playerShield = other.GetComponent<PlayerShield>();
                    playerController = other.GetComponent<PlayerController>();

                    ApplyEffect(); // Terapkan efek item ke player
                    Destroy(gameObject);// Hancurkan item setelah diambil
                }
            }
        
        private void ApplyEffect()
        {
            switch (itemType)
            {
                case ItemType.Oxygen:
                    OxygenManager.Instance.RefillOxygen(Amount);
                    break;
                case ItemType.Shield:
                    if (playerShield != null)
                    {
                        playerShield.ActiveShield(Duration);
                    }
                    break;
                case ItemType.SpeedBoost:
                    if (playerController != null)
                    {
                        playerController.ApplySpeedBoost(multiplier, Duration);
                    }
                    break;
            }
        }
    }
}

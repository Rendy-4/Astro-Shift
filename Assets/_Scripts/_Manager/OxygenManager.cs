using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AstroShift.Player;

namespace AstroShift.Manager
{
    public class OxygenManager : MonoBehaviour
    {


        [Header("Oxygen Settings")]
        [SerializeField] private float maxOxygen = 100f;
        [SerializeField] private Image oxygenBar;
        [SerializeField] private float oxygenDepletionRate = 5f;

        private float currentOxygen;
        private bool isDead = false;

        public static OxygenManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                currentOxygen = maxOxygen;
            }
            else 
            {
                Destroy(gameObject);
                return;
            }
        }

        private void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            FindOxygenUI();
            isDead = false;

            currentOxygen = maxOxygen; // Reset oksigen saat pindah scene
        }

        private void FindOxygenUI() {
            GameObject oxygenBarObj = GameObject.Find("Mask Oxygen");
            if (oxygenBarObj != null) {
                oxygenBar = oxygenBarObj.GetComponent<Image>();
                UpdateOxygenBar();
            } else {
                Debug.LogWarning("Oxygen bar UI tidak ditemukan di scene!");
            }
        }

        void Start()
        {
            UpdateOxygenBar();
        }

        void Update()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu") return; // Jangan kurangi oksigen di menu utama
            if (isDead) return;

            // Terus berkurang setiap detik
            currentOxygen -= oxygenDepletionRate * Time.deltaTime;
            currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);
            UpdateOxygenBar();

            if (currentOxygen <= 0f)
            {
                isDead = true;
                OnOxygenEmpty();
            }
        }

        private void UpdateOxygenBar()
        {
            if (oxygenBar != null)
                oxygenBar.fillAmount = currentOxygen / maxOxygen;
        }

        private void OnOxygenEmpty()
        {
            Debug.Log("Oksigen habis!");
            // Bisa tambahkan efek kematian atau restart level di sini
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null) player.Die();
        }

        // Dipanggil saat player mengambil item oksigen
        public void RefillOxygen(float amount)
        {
            currentOxygen = Mathf.Clamp(currentOxygen + amount, 0f, maxOxygen);
            isDead = false;
            UpdateOxygenBar();
            Debug.Log($"Oksigen diisi +{amount}. Sekarang: {currentOxygen}");
        }
    }
}
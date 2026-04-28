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
        private bool isGameScene = false;

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
            isGameScene = scene.name != "MainMenu";
            FindOxygenUI();
            isDead = false;

            currentOxygen = maxOxygen;
        }

        private void FindOxygenUI() {
            GameObject oxygenBarObj = GameObject.Find("Mask Oxygen");
            if (oxygenBarObj != null) {
                oxygenBar = oxygenBarObj.GetComponent<Image>();
                UpdateOxygenBar();
            }
        }

        void Start()
        {
            UpdateOxygenBar();
        }

        void Update()
        {
            if (!isGameScene || isDead) return;
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
            PlayerController player = Object.FindFirstObjectByType<PlayerController>();
            if (player != null) player.Die();
        }

        // Dipanggil saat player mengambil item oksigen
        public void RefillOxygen(float amount)
        {
            currentOxygen = Mathf.Clamp(currentOxygen + amount, 0f, maxOxygen);
            isDead = false;
            UpdateOxygenBar();
        }
    }
}
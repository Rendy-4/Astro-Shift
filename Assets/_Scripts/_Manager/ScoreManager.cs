using UnityEngine;
using TMPro;

namespace AstroShift.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("Score Settings")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Transform playerTransform;

        private float StratPosX;
        private int currentScore;
        
        void Start()
        {
            if (playerTransform == null) {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
            StratPosX = playerTransform.position.x;
        }

        void Update()
        {
            if (playerTransform != null) {
                currentScore = Mathf.FloorToInt(playerTransform.position.x - StratPosX);
                scoreText.text = "Score: " + currentScore.ToString();
            }
        }

        public int GetCurrentScore() {
            return currentScore;
        }
    }
}


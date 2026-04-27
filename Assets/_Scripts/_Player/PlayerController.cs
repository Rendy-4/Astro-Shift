using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
using AstroShift.Core;
using AstroShift.Manager;

namespace AstroShift.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [Header("Gravity Settings")]
        [SerializeField] private float flipDuration = 1f;
        [SerializeField] private Transform charVisual;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private LayerMask groundLayer;

        [Header("References")]
        [SerializeField] private Transform FeetPosition;

        private Rigidbody2D rb;
        private PlayerParticle playerParticle;
        private PlayerShield playerShield;
        private Animator animator;

        private bool isDead = false;
        private bool isFlipping = false;
        private float originalSpeed;
        private bool isBuffered = false;
        private float bufferTime = 0f;
        private float bufferTimer = 0f;

        private Coroutine speedBoostCoroutine;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerParticle = GetComponent<PlayerParticle>();
            playerShield = GetComponent<PlayerShield>();
            animator = GetComponentInChildren<Animator>();

            originalSpeed = moveSpeed;
        }

        private void Start()
        {
            isFlipping = rb.gravityScale > 0;
        }

        private void Update() {
            if (bufferTimer > 0f) bufferTimer -= Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (isDead) return;
            HandleAutomaticMovement();
            playerParticle.HandleDustEffects(IsTouchingSurface(), rb.linearVelocity.x, isFlipping);

            if (isBuffered && IsTouchingSurface() && bufferTimer <= 0f)
            {
                isBuffered = false;
                bufferTimer = 0f;
                ExecuteSwitchGravity();
            }
        }

        private void HandleAutomaticMovement()
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }

        private bool IsTouchingSurface()
        {
            return Physics2D.OverlapBox(FeetPosition.position, new Vector2(0.5f, 0.2f), 0, groundLayer);
        }

        public void ApplySpeedBoost(float multiplier, float duration)
        {
            if (speedBoostCoroutine != null)
            {
                StopCoroutine(speedBoostCoroutine);
            }
            speedBoostCoroutine = StartCoroutine(SpeedBoostDuration(multiplier, duration));
        }

        private IEnumerator SpeedBoostDuration(float multiplier, float duration)
        {
            moveSpeed = originalSpeed * multiplier;
            yield return new WaitForSeconds(duration);
            moveSpeed = originalSpeed;
            speedBoostCoroutine = null;
        }

        public void SwitchGravity()
        {
            if (isDead) return;
            if (IsTouchingSurface())
            {
                ExecuteSwitchGravity();
            }
            else
            {
                isBuffered = true;
                bufferTimer = bufferTime;
            }
                
        }

        private void ExecuteSwitchGravity()
        {
            AstroShift.Manager.LevelStastManager.clickCount++;
            Vector3 spawnPos = FeetPosition.position;
            bool wasAtTop = !isFlipping;

            rb.gravityScale *= -1;
            isFlipping = rb.gravityScale > 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            float targetRotation = isFlipping ? 0f : 180f;
            float targetFlip = isFlipping ? 1f : -1f;
            float targetFeetY = isFlipping ? -0.78f : 0.83f;

            FeetPosition.localPosition = new Vector3(FeetPosition.localPosition.x, targetFeetY, FeetPosition.localPosition.z);

            charVisual.DOKill();
            charVisual.DORotate(new Vector3(0, 0, targetRotation), flipDuration).SetEase(Ease.InOutSine).SetLink(gameObject);
            
            charVisual.DOScaleX(targetFlip, flipDuration).SetEase(Ease.InOutSine).SetLink(gameObject);

            playerParticle.SpawnSwitchDust(spawnPos, wasAtTop);
        }

        public void ManualDisable()
        {
            isDead = true;
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false; 
        }

        public void Die(GameObject killer = null)
        {
            if (isDead) return;

            Debug.Log($"Die dipanggil. Shield; {playerShield}, IsActive; {playerShield?.IsActive}");
            if (playerShield != null && playerShield.IsActive)
            {
                playerShield.BreakShield();
                
                if (killer != null)
                {
                    Destroy(killer);
                }
                return; 
            }
            ManualDisable();
            if (SettingManager.Instance != null) SettingManager.Instance.PlayDeathSfx();
            if (animator != null) animator.SetBool("isDeath", true);
            AstroShift.Manager.LevelStastManager.attemptCount++;
            StartCoroutine(DieAfterAnimatoin());
        }

        private IEnumerator DieAfterAnimatoin()
        {
            yield return new WaitForSeconds(0.55f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDrawGizmos()
        {
            if (FeetPosition != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(FeetPosition.position, new Vector2(0.5f, 0.2f));
            }
        }
    }
}
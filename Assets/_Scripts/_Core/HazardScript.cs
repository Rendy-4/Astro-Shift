using UnityEngine;
using AstroShift.Core;
using AstroShift.Player;

namespace AstroShift.Core
{
    public class HazardScript : MonoBehaviour
    {
        private Collider2D _collider;
        private Transform player;

        private void Start() {
            _collider = GetComponent<Collider2D>();
            
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            player = playerObj.transform;
        }

        private void Update() {
            if (player == null || _collider == null) return;

            _collider.enabled = Vector2.Distance(transform.position, player.position) < 15f;
        }

        private void OnCollisionEnter2D(Collision2D other) {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Die(this.gameObject); // Memanggil metode Die() pada player
                }   
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Die(this.gameObject); // Memanggil metode Die() pada player
            }
        }
    }
}


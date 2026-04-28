using UnityEngine;
using AstroShift.Core;
using AstroShift.Player;

namespace AstroShift.Core
{
    public class HazardScript : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other) {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Die(this.gameObject);
                }   
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Die(this.gameObject);
            }
        }
    }
}


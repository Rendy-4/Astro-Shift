using UnityEngine;

namespace AstroShift.Core
{
    public class KillZone : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Die(null);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Die(null);
            }
        }
    }    
}
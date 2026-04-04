using UnityEngine;

public class HazardScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Die(); // Memanggil metode Die() pada player
            }
            
    }
}

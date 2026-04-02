using UnityEngine;

namespace AstroShift.Core
{
    public class DeathLogic : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player menyentuh block!");
                // Di sini kamu bisa menambahkan logika untuk mengurangi nyawa, memulai ulang level, atau efek kematian lainnya
            }
        }
    }
}


using UnityEngine;
using System.Collections.Generic;

namespace AstroShift.Core
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialSize = 10;
        
        private Queue<GameObject> pool = new Queue<GameObject>();

        private void Awake()
        {
            // Pre-warm: Mengisi pool di awal agar tidak terjadi lag saat gameplay
            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject obj;

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
            }
            else
            {
                // Jika stok habis, buat baru
                obj = Instantiate(prefab);
            }

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}

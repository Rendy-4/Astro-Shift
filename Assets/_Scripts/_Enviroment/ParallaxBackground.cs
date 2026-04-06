using UnityEngine;

namespace AstroShift.Environment
{
    public class ParallaxBackground : MonoBehaviour
    {
        private Transform cameraTransform;
        private Vector3 cameraStartPos;
        private float distance;

        private GameObject[] background;
        private Material[] materials;
        private float[] backgroundSpeed;

        // 1. Perbaikan agar muncul di Inspector
        [SerializeField] [Range(0.01f, 0.5f)] 
        private float parallaxSpeed = 0.02f; 

        private float farthestBackground;

        private void Start() {
            cameraTransform = Camera.main.transform;
            cameraStartPos = cameraTransform.position;

            int backgroundCount = transform.childCount;
            materials = new Material[backgroundCount];
            backgroundSpeed = new float[backgroundCount];
            background = new GameObject[backgroundCount];

            for (int i = 0; i < backgroundCount; i++) {
                background[i] = transform.GetChild(i).gameObject;
                // Gunakan Renderer agar lebih fleksibel (bisa Sprite atau Mesh)
                materials[i] = background[i].GetComponent<Renderer>().material;
            }

            // Panggil fungsi hitung kecepatan
            CalculateBackgroundSpeed(backgroundCount);
        }

        private void CalculateBackgroundSpeed(int backgroundCount) {
            farthestBackground = 0;

            // Cari jarak terjauh
            for (int i = 0; i < backgroundCount; i++) {
                float zDist = background[i].transform.position.z - cameraTransform.position.z;
                if (zDist > farthestBackground) {
                    farthestBackground = zDist;
                }
            }

            // Hitung kecepatan berdasarkan jarak Z
            for (int i = 0; i < backgroundCount; i++) {
                float zDist = background[i].transform.position.z - cameraTransform.position.z;
                backgroundSpeed[i] = 1 - (zDist / farthestBackground);
            }
        }

        private void LateUpdate() {
            distance = cameraTransform.position.x - cameraStartPos.x;

            for (int i = 0; i < background.Length; i++) {
                float speed = backgroundSpeed[i] * parallaxSpeed;
                // Menggerakkan isi tekstur (Offset)
                materials[i].SetTextureOffset("_MainTex", new Vector2(distance * speed, 0));
            }
        }
    }
}
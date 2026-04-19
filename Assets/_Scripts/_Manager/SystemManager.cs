using UnityEngine;

namespace AstroShift.Manager
{
    public class SystemManager : MonoBehaviour
    {
        public static SystemManager Instance { get; private set; }
        private void Awake() {
            if (Instance == null) 
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } 
            else 
            {
                Destroy(gameObject);
            }
        }
    }
}
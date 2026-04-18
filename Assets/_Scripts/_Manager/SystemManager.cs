using UnityEngine;

namespace AstroShift.Manager
{
    public class SystemManager : MonoBehaviour
    {
        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}
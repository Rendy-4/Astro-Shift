using UnityEngine;

namespace AstroShift.Manager
{
    public class LevelStastManager : MonoBehaviour
    {
        public static int attemptCount = 1;
        public static int clickCount = 0;

        public static void ResetStats()
        {
            attemptCount = 1;
            clickCount = 0;
        }
    }
}
using UnityEngine;
using AstroShift.Player;

namespace AstroShift.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController Player;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Player.SwitchGravity();
            }
        }
    }
}


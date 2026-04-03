using UnityEngine;
using UnityEngine.InputSystem;
using AstroShift.Player;

namespace AstroShift.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController Player;
        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
            {
                Player.SwitchGravity();
            }
        }
    }
}


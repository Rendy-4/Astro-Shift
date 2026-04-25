using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using AstroShift.Player;

namespace AstroShift.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController Player;
        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale > 0)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (!IsPointerOverUIObject())
                    {
                        Player.SwitchGravity();
                    }
                    
                }
            }
        }

        private bool IsPointerOverUIObject() {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }
    }
}


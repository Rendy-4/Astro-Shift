using UnityEngine;

namespace AstroShift.Core
{
    public class HazardMover : MonoBehaviour
    {
        public enum MoveType {UpDown, LeftRight}

        [SerializeField] private MoveType moveType;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float moveDistance;
        
        private Vector3 startPos;

        private void Start() {
            startPos = transform.position;
        }

        private void Update() {
            float moveOffet = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

            if (moveType == MoveType.UpDown)
            {
                transform.position = startPos + Vector3.up * moveOffet;
            }
            else if (moveType == MoveType.LeftRight)
            {
                transform.position = startPos + Vector3.right * moveOffet;
            }
        }
    }    
}
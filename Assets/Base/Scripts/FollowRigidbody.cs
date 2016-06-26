using UnityEngine;

namespace Assets.Shared.Scripts
{
    public class FollowRigidbody : MonoBehaviour
    {
        public Transform Following;
        public float FollowSpeed;

        public bool LockX;
        public bool LockY;
        public bool LockZ;

        public Vector3 Offset;
        public bool SmoothFollow = true;

        private void FixedUpdate()
        {
            if (Following == null) return;
            var targetPosition = transform.position;

            if (!LockX) targetPosition.x = Following.position.x;
            if (!LockY) targetPosition.y = Following.position.y;
            if (!LockZ) targetPosition.z = Following.position.z;

            var distance = SmoothFollow ? FollowSpeed : 1;
            transform.position = Vector3.Lerp(transform.position, targetPosition + Offset, distance);
            transform.LookAt(transform.position - Offset);
        }
    }
}
using UnityEngine;

namespace Assets.Shared.Scripts
{
    public class Follow : MonoBehaviour
    {
        public Transform Following;
        public float FollowSpeed;

        public bool LockX;
        public bool LockY;
        public bool LockZ;

        public Vector3 Offset;
        public bool SmoothFollow = true;

        private void Update()
        {
            if (Following == null) return;
            var targetPosition = transform.position;

            if (!LockX) targetPosition.x = Following.position.x;
            if (!LockY) targetPosition.y = Following.position.y;
            if (!LockZ) targetPosition.z = Following.position.z;

            var distance = SmoothFollow ? Time.deltaTime*FollowSpeed : 1;
            transform.position = Vector3.Slerp(transform.position, targetPosition + Offset, distance);
            transform.LookAt(transform.position - Offset);
        }
    }
}
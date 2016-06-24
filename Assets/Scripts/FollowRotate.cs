using Assets.Shared.Scripts;
using UnityEngine;

namespace Core
{
    public class FollowRotate : MonoBehaviour
    {
        public Transform Follow;
        public Vector3 Axis;
        public float FollowSpeed;

        public float Offset;

        private void FixedUpdate()
        {
            var pivot = Vector3.zero + Follow.position.Multiply(Axis);
            var direction = (Follow.position - pivot).normalized;

            var distnace = Follow.position.Distance(pivot);
            var offset = Offset * (1 / GetComponent<Camera>().aspect);

            transform.position = Vector3.Lerp(transform.position, pivot + direction*(distnace + offset), Time.deltaTime*FollowSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-direction, Axis), Time.deltaTime*FollowSpeed);
        }
    }
}
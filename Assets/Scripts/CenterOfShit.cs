using UnityEngine;

namespace Assets.Scripts
{
    public class CenterOfShit : MonoBehaviour
    {
        [SerializeField] private Transform[] points;

        private void OnDrawGizmos()
        {
            var total = new Vector3();
            foreach (var point in points)
            {

                total += point.position;
            }
           
            Gizmos.DrawSphere(total / points.Length, .1f);
        }
    }
}
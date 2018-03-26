using System.Linq;
using Assets.Core.Extensions;
using Assets.Core.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class EnsureColliderIsInView : MonoBehaviour
    {
        [SerializeField] private Collider collider;

        private float lastAspect;

        private void Update()
        {
            var aspect = (float)Screen.height / Screen.width;

            if (aspect != lastAspect)
            {
                Recalculate(aspect);
                lastAspect = aspect;
            }
        }

        private void Recalculate(float aspect)
        {
            var vecViewportPairs = collider.bounds.GetVertices()
                .Select(v => Vector3.ProjectOnPlane(v, Camera.main.transform.forward))
                .Select(v => new { Vec = v, Viewport = Camera.main.WorldToViewportPoint(v) }).ToList();

            var verticalPoints = vecViewportPairs.OrderBy(v => v.Viewport.y).ToList();

            var horizontalPoints = vecViewportPairs.OrderBy(v => v.Viewport.x).ToList();

            var left = verticalPoints.First().Vec;
            var right = verticalPoints.Last().Vec;
            var bottom = horizontalPoints.First().Vec;
            var top = horizontalPoints.Last().Vec;

            var height = left.Distance(right);
            var width = bottom.Distance(top) * aspect;
            var hdist = height / 2 / Mathf.Tan(Mathf.PI * Camera.main.fieldOfView / 360);
            var wdist = width / 2 / Mathf.Tan(Mathf.PI * Camera.main.fieldOfView / 360);

            Camera.main.transform.position = -Camera.main.transform.forward * Mathf.Max(hdist, wdist);
            RenderSettings.fogStartDistance = Camera.main.transform.position.magnitude;
            RenderSettings.fogEndDistance = RenderSettings.fogStartDistance + 32;
        }
    }
}
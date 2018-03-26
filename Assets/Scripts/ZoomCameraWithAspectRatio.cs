using UnityEngine;

namespace Assets.Scripts
{
    public class ZoomCameraWithAspectRatio : MonoBehaviour
    {
        [SerializeField] private Vector3 defaultPosition;

        private void Update()
        {
            var aspect = (float)Screen.width / Screen.height;
            Camera.main.transform.position = defaultPosition + Camera.main.transform.forward * (aspect - 1) * 50;
            RenderSettings.fogStartDistance = Camera.main.transform.position.magnitude;
            RenderSettings.fogEndDistance = RenderSettings.fogStartDistance + 32;
        }
    }
}
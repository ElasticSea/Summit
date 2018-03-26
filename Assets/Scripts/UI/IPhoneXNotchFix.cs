using UnityEngine;
using UnityEngine.iOS;

namespace Assets.Scripts.UI
{
    public class IPhoneXNotchFix : MonoBehaviour
    {
        private void Awake()
        {
            if (Device.generation != DeviceGeneration.iPhoneX)
            {
                DestroyImmediate(gameObject);
            }
        }
    }
}
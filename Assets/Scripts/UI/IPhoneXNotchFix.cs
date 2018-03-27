using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace Assets.Scripts.UI
{
    public class IPhoneXNotchFix : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_IOS
            if (Device.generation != DeviceGeneration.iPhoneX)
            {
                DestroyImmediate(gameObject);
            }
#else
            DestroyImmediate(gameObject);
#endif
        }
    }
}
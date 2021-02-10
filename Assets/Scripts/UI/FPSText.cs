using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class FPSText : MonoBehaviour
    {
        [SerializeField] private Text text;
        // [SerializeField] private FpsCounter fpsCounter;

        private void Update()
        {
            // text.text = fpsCounter.Fps.ToString("0.## Fps");
        }
    }
}
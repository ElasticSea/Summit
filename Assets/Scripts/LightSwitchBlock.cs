using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class LightSwitchBlock : MonoBehaviour
    {
        public delegate void OnSwitchClickedHandler();
        public event OnSwitchClickedHandler OnSwitchClicked;


        public void OnMouseDown()
        {
            if (OnSwitchClicked != null) OnSwitchClicked();
        }
    }
}
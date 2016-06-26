using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class LightSwitch : MonoBehaviour
    {

        public delegate void OnSwitchClickedHandler(LightSwitch lightSwitch);
        public event OnSwitchClickedHandler OnSwitchClicked;

        public int Levels = 1;
        private int currentLevel;

        public void Switch()
        {
            setLevel((currentLevel + 1) % (Levels + 1));
        }

        private void setLevel(int newLevel)
        {
            currentLevel = newLevel;
            transform.SetY(currentLevel * 0.1f);
        }

        public void OnMouseDown()
        {
            if (OnSwitchClicked != null) OnSwitchClicked(this);
        }
    }
}
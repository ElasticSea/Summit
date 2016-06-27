using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class LightSwitch : MonoBehaviour
    {
        public delegate void OnSwitchClickedHandler(LightSwitch lightSwitch);
        public event OnSwitchClickedHandler OnSwitchClicked;

        public Transform ScalingPart;
        public int Levels = 1;
        private int currentLevel;

        public void Switch()
        {
            setLevel((currentLevel + 1) % (Levels + 1));
        }

        private void setLevel(int newLevel)
        {
            currentLevel = newLevel;
            ScalingPart.DOScaleY(currentLevel + 1, 0.33f).SetEase(Ease.OutQuad);
        }

        public void OnMouseDown()
        {
            if (OnSwitchClicked != null) OnSwitchClicked(this);
        }
    }
}
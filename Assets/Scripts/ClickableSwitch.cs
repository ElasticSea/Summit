using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class ClickableSwitch : MonoBehaviour
    {
        public delegate void OnSwitchClickedHandler();
        public event OnSwitchClickedHandler OnSwitchClicked;

        public int Levels = 1;
        public float Width;
        public float Height;
        protected int currentLevel;

        public float TransitionTime;
        public Ease TransitionEase;

        public void Click()
        {
            if (OnSwitchClicked != null) OnSwitchClicked();
        }

        public void Switch()
        {
            var newLevel = (currentLevel + 1) % (Levels + 1);
            setLevel(newLevel);
            currentLevel = newLevel;
        }

        protected abstract void setLevel(int newLevel);
    }
}
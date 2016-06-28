using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class ClickableSwitch : MonoBehaviour
    {
        public delegate void OnSwitchClickedHandler(ClickableSwitch clickable);
        public event OnSwitchClickedHandler OnSwitchClicked;

        public int Levels = 1;
        public float Width;
        public float Height;
        protected int currentLevel;

        public float TransitionTime;
        public Ease TransitionEase;

        public void Click()
        {
            if (OnSwitchClicked != null) OnSwitchClicked(this);
        }

        public void Switch()
        {
            SetLeveLInternal((currentLevel + 1) % (Levels + 1));
        }

        private void SetLeveLInternal(int newLevel)
        {
            setLevel(newLevel);
            currentLevel = newLevel;
        }

        public void Activate()
        {
            SetLeveLInternal(Levels);
        }

        protected abstract void setLevel(int newLevel);

        public bool IsDown()
        {
            return currentLevel == 0;
        }
    }
}
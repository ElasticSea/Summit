using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Switch : MonoBehaviour
    {
        public Transform PullablePart;
        public int Levels = 1;
        public float Width;
        public float Height;
        protected int elevation;
        public float TransitionTime;
        public Ease TransitionEase;

        public void OnMouseDown()
        {
            Click();
        }

        public delegate void OnSwitchClickedHandler(Switch clickable);

        public event OnSwitchClickedHandler OnSwitchClicked;

        public void Click()
        {
            if (OnSwitchClicked != null) OnSwitchClicked(this);
        }

        public void FlipSwitch()
        {
            elevate((elevation + 1) % (Levels + 1));
        }

        public void elevate(int elevation)
        {
            PullablePart.DOLocalMoveY(elevation, TransitionTime).SetEase(TransitionEase);
            this.elevation = elevation;
        }

        public bool IsDown()
        {
            return elevation == 0;
        }
    }
}
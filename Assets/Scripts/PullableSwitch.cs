using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class PullableSwitch : ClickableSwitch
    {
        public Transform PullablePart;

        public void OnMouseDown()
        {
            Click();
        }

        protected override void setLevel(int newLevel)
        {
            PullablePart.DOLocalMoveY(newLevel, TransitionTime).SetEase(TransitionEase);
        }
    }
}
using Assets.Shared.Scripts;
using DG.Tweening;
using UnityEngine;
using Vexe.Runtime.Extensions;

namespace Assets.Scripts
{
    public class CascadeClickableSwitch : ClickableSwitch
    {
        public LightSwitchBlock BlockPrefab;

        private void Start()
        {
            transform.DestroyChildren();
            for (int i = 0; i < Levels; i++)
            {
                var child = Instantiate(BlockPrefab);
                child.transform.SetParent(transform);
                var scale = (1f/ Levels) * (Levels - i) * Width;
                child.transform.localScale = new Vector3(scale, Height, scale);
                child.transform.localPosition = Vector3.zero;
                child.OnSwitchClicked += Click;
            }
        }

        protected override void setLevel(int newLevel)
        {
            currentLevel = newLevel;
            var children = currentLevel == 0 ? transform.Children() : transform.GetChild(currentLevel - 1).Yield();
            children.ForEach(child => child.DOScaleY((currentLevel + 1) * Height, TransitionTime).SetEase(TransitionEase));
        }
    }
}
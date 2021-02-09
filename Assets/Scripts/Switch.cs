using System;
using System.Linq;
using DG.Tweening;
using ElasticSea.Framework.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    public class Switch : MonoBehaviour
    {
        public Transform SwitchPart;
        public Transform PedestalPart;
        public int Levels = 1;
        public float TransitionTime;
        public Ease TransitionEase;

        private int elevation;
        private Tweener weener;

        public int Elevation
        {
            get { return elevation; }
            set
            {
                SwitchPart.SetLocalY(value);
                elevation = value;
            }
        }

        private void Awake()
        {
            GetComponentsInChildren<Collider>()
                .Select(c => c.gameObject)
                .Distinct()
                .ForEach(go =>
                    go.AddComponent<ColliderClick>().OnClick += () =>
                    {
                        AnimatePedestal();
                        OnSwitchClicked(this);
                    }
                );
        }

        private void AnimatePedestal()
        {
            PedestalPart.SetLocalScaleY(1);
            weener?.Kill();
            weener = PedestalPart.DOScaleY(3, .2f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
        }

        public void AnimateElevation(int value)
        {
            SwitchPart.DOLocalMoveY(value, TransitionTime).SetEase(TransitionEase);
            elevation = value;
        }

        public event Action<Switch> OnSwitchClicked = @switch => {};

        public void FlipSwitch()
        {
            AnimateElevation((Elevation + 1) % (Levels + 1));
        }

        public bool IsDown() => Elevation == 0;
    }
}
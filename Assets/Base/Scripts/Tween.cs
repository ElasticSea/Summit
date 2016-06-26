using UnityEngine;

namespace Assets.Scripts
{
    public class Tween : MonoBehaviour
    {
        public delegate void OnEventHandler();

        public delegate void OnTweenHandler(float progress, float tweenValue);

        public event OnEventHandler OnStart;
        public event OnEventHandler OnFinish;
        public event OnTweenHandler OnTween;

        public AnimationCurve curve;

        private enum State
        {
            Playing,
            Idle
        }

        private State state = State.Idle;
        private float duration;
        private float startTime;
        private float deltaTime;

        public void startTween(float duration)
        {
            this.duration = duration;
            deltaTime = 0;
            state = State.Playing;
            if (OnStart != null) OnStart();
        }

        public void Update()
        {
            if (state.Equals(State.Playing))
            {
                deltaTime += Time.deltaTime;
                if (deltaTime < duration)
                {
                    if (OnTween != null)
                    {
                        float curveTime = curve[curve.length - 1].time;

                        float progress = deltaTime / duration;
                        OnTween(progress, curve.Evaluate(progress * curveTime));
                    }
                }
                else
                {
                    state = State.Idle;
                    if (OnFinish != null) OnFinish();
                }
            }
        }
    }
}
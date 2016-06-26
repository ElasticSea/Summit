using UnityEngine;

namespace Assets.Shared.Scripts
{
    internal class Timer : MonoBehaviour
    {
        public delegate void OnTimeHandler();

        public int Sign { get; private set; }
        public float Time { get; set; }
        public bool Active { get; private set; }

        public event OnTimeHandler OnTimeEnd;

        public void setTimer(float time, int sign)
        {
            Time = time;
            Sign = sign;
        }

        public void startTimer()
        {
            Active = true;
        }

        private void Update()
        {
            if (!Active) return;

            Time += UnityEngine.Time.deltaTime*Sign;

            if (Time < 0)
            {
                Active = false;
                if (OnTimeEnd != null) OnTimeEnd();
            }
        }

        public void stop()
        {
            Active = false;
        }
    }
}
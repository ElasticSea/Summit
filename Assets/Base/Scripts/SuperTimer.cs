using System;
using System.Collections;
using UnityEngine;

namespace UnitySharedLibrary.Scripts
{
    public class SuperTimer : MonoBehaviour
    {
        public delegate void OnElapsedHandler();

        private static float startTime;

        public float Interval;
        public bool AutoReset;

        public float Time
        {
            get { return Interval - (UnityEngine.Time.time - startTime); }
        }

        public event OnElapsedHandler OnElapsed;

        public void StartCouroutine()
        {
            StartCoroutine(StartCouroutine(() =>
            {
                if (OnElapsed != null)
                {
                    OnElapsed();
                }
                if (AutoReset)
                {
                    StartCouroutine();
                }
            }));
        }

        private IEnumerator StartCouroutine(Action callback)
        {
            startTime = UnityEngine.Time.time;
            yield return new WaitForSeconds(Interval);
            callback();
        }
    }
}
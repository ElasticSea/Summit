using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LevelText : MonoBehaviour
    {
        public Text Text;

        private float showTime;
        private float triggerTime;

        public void TriggerLevel(string name, float showTime)
        {
            this.showTime = showTime;
            triggerTime = Time.time;
            Text.text = name;
        }

        private void Update()
        {
            GetComponent<Animator>().SetBool("Show", Time.time - triggerTime < showTime);
        }
    }
}
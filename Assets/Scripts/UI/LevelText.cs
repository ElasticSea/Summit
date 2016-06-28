using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LevelText : MonoBehaviour
    {
        public Text Text;

        public void TriggerLevel(string name)
        {
            Text.text = name;
            GetComponent<Animator>().SetTrigger("Show");
        }
    }
}
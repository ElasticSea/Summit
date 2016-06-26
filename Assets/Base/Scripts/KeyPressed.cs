using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Shared.Scripts
{
    public class KeyPressed : MonoBehaviour
    {
        public KeyCode keyCode;

        [Serializable]
        public class OnKeyPressed : UnityEvent { }

        public OnKeyPressed onKeyPressed;
        private void Update()
        {
            if (Input.GetKeyDown(keyCode) && onKeyPressed != null) onKeyPressed.Invoke();
        }
    }
}
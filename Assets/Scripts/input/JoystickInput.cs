using Assets.Scripts.input;
using UnityEngine;
using UnityEngine.UI;

namespace Shared.Scripts.input
{
    public class JoystickInput : IGameInput
    {
        private Vector2 direction;

        public JoystickInput(Joystick joystick)
        {
            joystick.OnValueChange.AddListener(value => Direction = value);
        }

        public Vector2 Direction { get; set; }
    }
}
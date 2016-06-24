using System.Linq;
using Assets.Scripts.input;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class JoystickToDpad : IGameInput
    {
        private readonly Joystick joystick;

        private readonly Vector2[] directions =
        {
            new Vector2(1,0), 
            new Vector2(1,1), 
            new Vector2(0,1), 
            new Vector2(-1,1), 
            new Vector2(-1,0), 
            new Vector2(-1,-1),
            new Vector2(0,-1),
            new Vector2(1,-1),
        };

        public JoystickToDpad(Joystick joystick)
        {
            this.joystick = joystick;
        }

        public Vector2 Direction
        {
            get
            {
                if (joystick.JoystickAxis.magnitude != 0)
                {
                    return directions
                        .Select(d => new {Direction = d, Angle = Vector2.Angle(joystick.JoystickAxis, d)})
                        .OrderBy(o => o.Angle)
                        .First()
                        .Direction * joystick.JoystickAxis.magnitude;
                }
                return Vector2.zero;
            }
        }
    }
}
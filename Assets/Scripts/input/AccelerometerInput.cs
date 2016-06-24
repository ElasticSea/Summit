using UnityEngine;

namespace Assets.Scripts.input
{
    public class AccelerometerInput : IGameInput
    {
        private readonly float multiplier;
        private readonly Vector3 offset;

        public AccelerometerInput(float multiplier, Vector3 offset)
        {
            this.multiplier = multiplier;
            this.offset = offset;
        }

        public Vector2 Direction
        {
            get
            {
                var accelerometer = (Input.acceleration - offset)*multiplier;
                accelerometer.x = Mathf.Clamp(accelerometer.x, -1, 1);
                accelerometer.y = Mathf.Clamp(accelerometer.y, -1, 1);
                accelerometer.z = Mathf.Clamp(accelerometer.z, -1, 1);
                return new Vector2(accelerometer.x, accelerometer.y);
            }
        }
    }
}
using UnityEngine;

namespace Assets.Scripts.input
{
    public class LowPassAccelerometerInput : IGameInput
    {
        private const float AccelerometerUpdateInterval = 1.0f/60.0f;
        private const float LowPassKernelWidthInSeconds = 0.1f;
        private const float LowPassFilterFactor = AccelerometerUpdateInterval/LowPassKernelWidthInSeconds; // tweakable
        private readonly AccelerometerInput input;

        public LowPassAccelerometerInput(float multiplier, Vector3 offset)
        {
            input = new AccelerometerInput(multiplier, offset);
            Direction = input.Direction;
        }
        public Vector2 Direction { get; set; }

        public void Update()
        {
            Direction = Vector3.Lerp(Direction, input.Direction, LowPassFilterFactor);
        }
    }
}
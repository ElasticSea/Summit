using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.input
{
    public class CompositeInput : IGameInput
    {
        private readonly List<IGameInput> inputs = new List<IGameInput>();

        public Vector2 Direction
        {
            get { return inputs.Aggregate(new Vector2(), (current, input) => current + input.Direction); }
        }

        public void Add(IGameInput input)
        {
            inputs.Add(input);
        }
    }
}
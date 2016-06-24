using UnityEngine;

namespace Assets.Scripts.input
{
    public class DefaultInput : IGameInput
    {
        public Vector2 Direction
        {
            get
            {
                var vertical = Input.GetAxis("Vertical");
                var horizontal = Input.GetAxis("Horizontal");
                return new Vector2(horizontal, vertical);
            }
        }
    }
}
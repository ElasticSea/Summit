using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class ColliderClick : MonoBehaviour
    {
        public event Action OnClick = () => { };

        private void OnMouseDown()
        {
            OnClick();
        }
    }
}
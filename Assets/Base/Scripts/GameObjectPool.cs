using System;
using UnityEngine;

namespace Shared.Scripts
{
    public class GameObjectPool : Pool<GameObject>
    {
        public GameObjectPool(int initialCapacity, Func<GameObject> creator, Transform container) : base(initialCapacity, creator)
        {
            OnPoolEnter += element =>
            {
                element.SetActive(false);
                element.transform.SetParent(container);
            };

            OnPoolExit += element =>
            {
                element.SetActive(true);
                element.transform.SetParent(null);
            };
        }
    }
}
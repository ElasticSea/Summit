using System;
using System.Collections.Generic;

namespace Shared.Scripts
{
    public class Pool<T>
    {
        public delegate void PoolHandler(T element);

        private readonly Func<T> creator;
        private readonly Stack<T> stack;

        public Pool(int initialCapacity, Func<T> creator)
        {
            this.creator = creator;

            stack = new Stack<T>();
            for (var i = 0; i < initialCapacity; i++) stack.Push(creator());
        }

        public T Element
        {
            get
            {
                var element = stack.Count > 0 ? stack.Pop() : creator();
                if (OnPoolExit != null) OnPoolExit(element);
                return element;
            }

            set
            {
                stack.Push(value);
                if (OnPoolEnter != null) OnPoolEnter(value);
            }
        }

        public event PoolHandler OnPoolEnter;
        public event PoolHandler OnPoolExit;
    }
}
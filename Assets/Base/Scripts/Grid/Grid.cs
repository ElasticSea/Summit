using System;
using UnityEngine;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Types;

namespace Assets.Base.Scripts.Grid
{
    public class Grid : BaseBehaviour
    {
        [Hide, SerializeField] private ArrayWithArray[] _grid;

        [Show]
        public int Width
        {
            get
            {
                if (_grid == null) return 0;
                return _grid.Length;
            }
        }

        [Show]
        public int Height
        {
            get
            {
                if (_grid == null) return 0;
                if (_grid.Length <= 0) return 0;
                if (_grid[0] == null) return 0;
                return _grid[0].Length;
            }
        }

        public GameObject this[int x, int y]
        {
            get { return _grid[x][y]; }
            set
            {
                _grid[x][y] = value;
                if (value != null)
                {
                    var cell = value.transform.GetOrAddComponent<GridCell>();
                    cell.X = x;
                    cell.Y = y;
                }
            }
        }

        public bool InBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }


        public void init(GameObject[,] gameObjects)
        {
            _grid = new ArrayWithArray[gameObjects.GetLength(0)];
            for (var x = 0; x < gameObjects.GetLength(0); x++)
            {
                if (_grid[x] == null)
                {
                    var arrayWithArray = new ArrayWithArray {array = new GameObject[gameObjects.GetLength(1)]};
                    _grid[x] = arrayWithArray;
                }
                for (var y = 0; y < gameObjects.GetLength(1); y++)
                {
                    this[x, y] = gameObjects[x, y];
                }
            }
        }
    }

    [Serializable]
    public class ArrayWithArray
    {
        [SerializeField] internal GameObject[] array;

        public GameObject this[int i]
        {
            get { return array[i]; }
            set { array[i] = value; }
        }

        public int Length
        {
            get { return array != null ? array.Length : 0; }
        }
    }
}
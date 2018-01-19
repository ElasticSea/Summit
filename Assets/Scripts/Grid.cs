using UnityEngine;
using Vexe.Runtime.Extensions;

namespace Assets.Scripts
{
    public class Grid<T> where T : Component
    {
        public readonly Cell[] cells;

        public int Width { get; }
        public int Height { get; }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;

            cells = new Cell[Width * Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    cells[i * Width + j] = new Cell(i, j);
                }
            }
        }

        public Cell this[int x, int y] => cells[x * Width + y];

        public bool InBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        public class Cell
        {
            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
            public T Value;
        }

        public void Align(float itemOffset)
        {
            var size = 1 + itemOffset;
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (this[x, y].Value != null)
                    {
                        var xpos = (x - (Width - 1) / 2f) * size;
                        var ypos = (y - (Height - 1) / 2f) * size;
                        this[x, y].Value.transform.localPosition = new Vector2(xpos, ypos).ToXZ();
                    }
                }
            }
        }
    }
}
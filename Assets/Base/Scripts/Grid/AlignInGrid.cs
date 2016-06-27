using Assets.Shared.Scripts;
using UnityEngine;
using Vexe.Runtime.Extensions;

namespace Assets.Base.Scripts.Grid
{
    [RequireComponent(typeof(IGridProvider))]
    public class AlignInGrid : MonoBehaviour
    {
        public int Width;
        public int Height;
        public float ItemOffset;

        public void Align()
        {
            transform.RemoveAllChildren();

            var grid = transform.GetOrAddComponent<Grid>();
            grid.init(new GameObject[Width, Height]);

            var size = 1 + ItemOffset;
            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    grid[x, y] = GetComponent<IGridProvider>().Provide(x, y);
                    grid[x, y].transform.SetParent(transform);

                    var xpos = (x - (Width - 1) / 2f) * size;
                    var ypos = (y - (Height - 1) / 2f) * size;
                    grid[x, y].transform.localPosition = new Vector2(xpos, ypos).ToXZ();
                }
            }
        }
    }
}
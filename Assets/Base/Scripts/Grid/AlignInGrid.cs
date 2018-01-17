using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Base.Scripts.Grid
{
    [RequireComponent(typeof(IGridProvider))]
    public class AlignInGrid : MonoBehaviour
    {
        public float ItemOffset;

        public void Align()
        {
            var provider = GetComponent<IGridProvider>();

            transform.RemoveAllChildren();

            var grid = transform.GetComponent<Grid>();
            grid.init(new GameObject[provider.Width, provider.Height]);

            var size = 1 + ItemOffset;
            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    grid[x, y] = provider.Provide(x, y);
                    if (grid[x, y] != null)
                    {
                        grid[x, y].transform.SetParent(transform);

                        var xpos = (x - (provider.Width - 1)/2f)*size;
                        var ypos = (y - (provider.Height - 1)/2f)*size;
                        grid[x, y].transform.localPosition = new Vector2(xpos, ypos).ToXZ();
                    }
                }
            }
        }
    }
}
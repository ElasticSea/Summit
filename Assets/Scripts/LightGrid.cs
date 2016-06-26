using System;
using System.Linq;
using Assets.Base.Scripts.Grid;
using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class LightGrid : MonoBehaviour
    {
        private Grid grid;

        public int Range;

        private void Start()
        {
            var children = transform.Children();
            grid = GetComponent<Grid>();
            children
                .Select(child => child.GetComponent<LightSwitch>())
                .ForEach(child => child.OnSwitchClicked += FlipSwitch);

        }

        private void FlipSwitch(LightSwitch lightSwitch)
        {
            var cell = lightSwitch.GetComponent<GridCell>();
            for (var x = cell.X - Range; x <= cell.X + Range; x++)
            {
                for (var y = cell.Y - Range; y <= cell.Y + Range; y++)
                {
                    if (grid.InBounds(x, y))
                    {
                        grid[x,y].GetComponent<LightSwitch>().Switch();
                    }
                }
            }
        }
    }
}
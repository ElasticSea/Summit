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
                .Select(child => child.GetComponent<ClickableSwitch>())
                .ForEach(child => child.OnSwitchClicked += () =>
                {
                    FlipSwitch(child);
                });

        }

        private void FlipSwitch(ClickableSwitch clickableSwitch)
        {
            var cell = clickableSwitch.GetComponent<GridCell>();
            for (var x = cell.X - Range; x <= cell.X + Range; x++)
            {
                for (var y = cell.Y - Range; y <= cell.Y + Range; y++)
                {
                    if (grid.InBounds(x, y))
                    {
                        grid[x,y].GetComponent<ClickableSwitch>().Switch();
                    }
                }
            }
        }
    }
}
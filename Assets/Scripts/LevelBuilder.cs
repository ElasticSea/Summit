using System.Collections.Generic;
using Assets.Base.Scripts.Grid;
using Newtonsoft.Json;
using UnityEngine;
using Vexe.Runtime.Extensions;

namespace Assets.Scripts
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField, TextArea(3,10)] private string Blueprint;
        [SerializeField] private Switch[] SwitchPrefabs;
        [SerializeField] private SwitchGrid SwitchGridPrefab;
        [SerializeField] private List<string[,]> levels;

        public int AvailableLevels => levels.Count;

        private void Awake()
        {
            levels = JsonConvert.DeserializeObject<List<string[,]>>(Blueprint);
        }

        public SwitchGrid CreateGrid(int levelIndex)
        {
            var level = levels[levelIndex];
            var switchGrid = transform.InstantiateChild(SwitchGridPrefab);

            var width = level.GetLength(0);
            var height = level.GetLength(1);

            var grid = new Grid<Switch>(width, height);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var element = CreateElement(level[x, y], x, y);

                    if (element != null)
                    {
                        var sw = switchGrid.transform.InstantiateChild(SwitchPrefabs[element.Type - 1]);
                        sw.name = "Switch " + level + " [" + x + ", " + y + "]";
                        sw.Elevation = element.Elevation;
                        grid[x, y].Value = sw;
                    }
                }
            }

            grid.Align(1);
            switchGrid.Init(grid);

            return switchGrid;
        }

        private Element CreateElement(string s, int x, int y)
        {
            if (s == "NA") return null;

            return new Element
            {
                Type = int.Parse(s[0].ToString()),
                Elevation = int.Parse(s[1].ToString())
            };
        }

        class Element
        {
            public int Type;
            public int Elevation;
        }
    }
}
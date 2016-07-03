using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelBuilder : MonoBehaviour
    {
        [TextArea(3,10)]
        public string Blueprint;
        public GameObject[] Pistons;

        private Element[,] instances;
        private List<string[,]> levels;

        public List<string[,]> Levels
        {
            get
            {
                if (levels == null)
                {
                    return JsonConvert.DeserializeObject<List<string[,]>>(Blueprint);
                }
                return levels;
            }
        }

        public Element[,] FillBlueprint(string[,] current)
        {
            var elements = new Element[current.GetLength(0), current.GetLength(1)];
            for (var x = 0; x < elements.GetLength(0); x++)
            {
                for (var y = 0; y < elements.GetLength(1); y++)
                {
                    elements[x, y] = CreateElement(current[x, y], x, y);
                }
            }
            return elements;
        }

        private Element CreateElement(string s, int x, int y)
        {
            if (s == "NA") return null;

            var level = int.Parse(s[0].ToString());
            var currentLevel = int.Parse(s[1].ToString());

            var instantiate = Instantiate(Pistons[level-1]);
            instantiate.name = "Switch " + level + " [" + x + ", " + y + "]";
            return new Element(instantiate, currentLevel);
        }
    }


    public class Element
    {
        public readonly int CurrentElevation;
        public readonly GameObject Prefab;

        public Element(GameObject prefab, int currentElevation)
        {
            Prefab = prefab;
            CurrentElevation = currentElevation;
        }
    }
}
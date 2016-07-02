using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelBuilder : MonoBehaviour, IEnumerator<Element[,]>
    {
        [TextArea(3,10)]
        public string LevelsJson;
        public GameObject[] BlockPrefabs;

        private Element[,] instances;
        private IEnumerator<Element[,]> levelIterator;

        public bool MoveNext()
        {
            return levelIterator.MoveNext();
        }

        public void Reset()
        {
            levelIterator.Reset();
        }

        public Element[,] Current
        {
            get { return levelIterator.Current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            levelIterator.Dispose();
        }

        private void Start()
        {
            var levels = JsonConvert.DeserializeObject<List<string[,]>>(LevelsJson);
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            levelIterator = levels.ToList().Select<string[,],Element[,]>(FillLevelBlueprint).GetEnumerator();
        }

        private Element[,] FillLevelBlueprint(string[,] current)
        {
            var instances = new Element[current.GetLength(0), current.GetLength(1)];
            for (var x = 0; x < instances.GetLength(0); x++)
            {
                for (var y = 0; y < instances.GetLength(1); y++)
                {
                    instances[x, y] = ReadBlueprintCell(current[x, y], x, y);
                }
            }
            return instances;
        }

        private Element ReadBlueprintCell(string s, int x, int y)
        {
            if (s == "N_A") return null;

            var level = int.Parse(s[1].ToString());
            var currentLevel = int.Parse(s[2].ToString());

            var instantiate = Instantiate(BlockPrefabs[level-1]);
            instantiate.name += " [" + x + ", " + y + "]";
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
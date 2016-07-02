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
        public string Blueprint;
        public GameObject[] Pistons;

        private Element[,] instances;
        private IEnumerator<Element[,]> enumerator;

        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }

        public void Reset()
        {
            enumerator.Reset();
        }

        public Element[,] Current
        {
            get { return enumerator.Current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            enumerator.Dispose();
        }

        private void Start()
        {
            var levels = JsonConvert.DeserializeObject<List<string[,]>>(Blueprint);
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            enumerator = levels.ToList().Select<string[,],Element[,]>(FillBlueprint).GetEnumerator();
        }

        private Element[,] FillBlueprint(string[,] current)
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
            if (s == "N_A") return null;

            var level = int.Parse(s[1].ToString());
            var currentLevel = int.Parse(s[2].ToString());

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
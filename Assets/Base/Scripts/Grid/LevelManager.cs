using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.UI;
using Assets.Shared.Scripts;
using Assets.Shared.Scripts.tuple;
using UnityEngine;
using Vexe.Runtime.Extensions;

namespace Assets.Base.Scripts.Grid
{
    public class LevelManager : MonoBehaviour, IGridProvider
    {
        public LevelText LevelText;
        public Level[] Levels;
        public int Width { get { return levelIterator.Current.Width; } }
        public int Height { get { return levelIterator.Current.Height; } }

        private List<Level>.Enumerator levelIterator;
        private Dictionary<Tuple<int, int>, Tuple<GameObject, bool>> instances;
        private LightGrid lightGrid;


        private void Start()
        {
            lightGrid = GetComponent<LightGrid>();
            lightGrid.OnPuzzleSolved += EndLevel;
            levelIterator = Levels.ToList().GetEnumerator();

            InitNextLevel();
        }

        private void EndLevel()
        {
            lightGrid.DeInit();
            Invoke("InitNextLevel", 1);
        }

        private void InitNextLevel()
        {
            if (levelIterator.MoveNext())
            {
                instances = levelIterator.Current.Elements.ToDictionary(it => new Tuple<int, int>(it.X, it.Y),
                    e =>
                    {
                        var instantiate = Instantiate(e.Prefab);
                        instantiate.name += " [" + e.X + ", " + e.Y + "]";
                        return new Tuple<GameObject, bool>(instantiate, e.active);
                    });

                PrepareLevel();
                Invoke("StartLevel", 3);
            }
            else
            {
                throw new InvalidOperationException("GAME OVER");
            }

        }

        public GameObject Provide(int x, int y)
        {
            var key = new Tuple<int, int>(x, y);
            return instances.ContainsKey(key) ? instances[key].Item1 : null;
        }

        private void PrepareLevel()
        {
            GetComponent<AlignInGrid>().Align();
            LevelText.TriggerLevel("Level "+ (Levels.IndexOf(levelIterator.Current) + 1).ToRoman());
        }

        private void StartLevel()
        {
            foreach (var value in instances.Values)
            {
                if(value.Item2) value.Item1.GetComponent<PullableSwitch>().Activate();
            }

            lightGrid.Init();
        }
    }

    [Serializable]
    public class Level
    {
        public int Width;
        public int Height;

        public Element[] Elements;
    }

    [Serializable]
    public class Element
    {
        public int X;
        public int Y;
        public GameObject Prefab;
        public bool active;
    }
}
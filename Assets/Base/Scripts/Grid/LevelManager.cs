using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.UI;
using Assets.Shared.Scripts;
using Assets.Shared.Scripts.tuple;
using UnityEngine;

namespace Assets.Base.Scripts.Grid
{
    [RequireComponent(typeof(LevelBuilder))]
    public class LevelManager : MonoBehaviour, IGridProvider
    {
        public int Width { get { return builder.Current.GetLength(0); } }
        public int Height { get { return builder.Current.GetLength(1); } }

        public LevelText LevelText;

        private LightGrid lightGrid;
        private List<Element[,]> levels;
        private LevelBuilder builder;
        private int level;


        private void Start()
        {
            builder = GetComponent<LevelBuilder>();
            lightGrid = GetComponent<LightGrid>();
            lightGrid.OnPuzzleSolved += EndLevel;
            InitNextLevel();
        }

        private void EndLevel()
        {
            lightGrid.DeInit();
            Invoke("InitNextLevel", 1);
        }

        private void InitNextLevel()
        {
            if (builder.MoveNext())
            {
                level++;
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
            var element = builder.Current[x, y];
            return element != null ? element.Prefab : null;
        }

        private void PrepareLevel()
        {
            GetComponent<AlignInGrid>().Align();
            LevelText.TriggerLevel("Level "+ level.ToRoman());
        }

        private void StartLevel()
        {
            foreach (var instance in builder.Current)
            {
                if (instance != null)
                    instance.Prefab.GetComponent<PullableSwitch>().Activate(instance.CurrentElevation);
            }

            lightGrid.Init();
        }
    }
}
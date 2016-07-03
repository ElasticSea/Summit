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
        private const float transitionTime = 2f;
        public int Width { get { return levels[CurrentLevel].GetLength(0); } }
        public int Height { get { return levels[CurrentLevel].GetLength(1); } }

        public LevelText LevelText;

        private PistonGrid _pistonGrid;
        private List<string[,]> levels;
        private Element[,] levelPrefabs;

        public int CurrentLevel
        {
            get { return PlayerPrefsSerializer.Load<int>("CurrentLevel"); }
            set
            {
                PlayerPrefsSerializer.Save("CurrentLevel", value);
            }
        }

        public int MaxLevel
        {
            get { return PlayerPrefsSerializer.Load<int>("MaxLevel"); }
            set
            {
                PlayerPrefsSerializer.Save("MaxLevel", value);
            }
        }

        private void Start()
        {
            levels = GetComponent<LevelBuilder>().Levels;
            _pistonGrid = GetComponent<PistonGrid>();
            _pistonGrid.OnPuzzleSolved += EndLevel;
            InitNextLevel();
        }

        private void EndLevel()
        {
            _pistonGrid.DeInit();
            CurrentLevel++;
            Invoke("InitNextLevel", 1);
        }

        private void InitNextLevel()
        {
            if (CurrentLevel < levels.Count - 1)
            {
                MaxLevel = Math.Max(CurrentLevel, MaxLevel);
                InitLevel();
            }
            else
            {
                throw new InvalidOperationException("GAME OVER");
            }

        }

        private void InitLevel()
        {
            levelPrefabs = GetComponent<LevelBuilder>().FillBlueprint(levels[CurrentLevel]);
            PrepareLevel();
            Invoke("StartLevel", transitionTime);
        }

        public GameObject Provide(int x, int y)
        {
            var element = levelPrefabs[x, y];
            return element != null ? element.Prefab : null;
        }

        private void PrepareLevel()
        {
            GetComponent<AlignInGrid>().Align();
            LevelText.TriggerLevel("Level "+ (CurrentLevel + 1).ToRoman(), transitionTime);
        }

        private void StartLevel()
        {
            foreach (var instance in levelPrefabs)
            {
                if (instance != null)
                    instance.Prefab.GetComponent<Switch>().elevate(instance.CurrentElevation);
            }

            _pistonGrid.Init();
        }

        public void NextLevel()
        {
            if (CanNextLevel())
                SetLevel(CurrentLevel + 1);
        }

        public bool CanNextLevel()
        {
            return CurrentLevel < MaxLevel;
        }

        public void RestartLevel()
        {
            SetLevel(CurrentLevel);
        }

        public void PreviousLevel()
        {
            if(CanPreviousLevel())
            SetLevel(CurrentLevel - 1);
        }

        public bool CanPreviousLevel()
        {
            return CurrentLevel > 0;
        }

        private void SetLevel(int level)
        {
            CancelInvoke();
            CurrentLevel = level;
            InitLevel();
        }
    }
}
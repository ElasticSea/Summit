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
    [RequireComponent(typeof(LevelBuilder), typeof(ThemeManager))]
    public class LevelManager : MonoBehaviour, IGridProvider
    {
        private const float transitionTime = 2f;
        public int Width { get { return levels[CurrentLevel].GetLength(0); } }
        public int Height { get { return levels[CurrentLevel].GetLength(1); } }

        public LevelText LevelText;

        private PistonGrid _pistonGrid;
        private List<string[,]> levels;
        private Element[,] levelPrefabs;
        private ThemeManager themeManager;

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
            themeManager = GetComponent<ThemeManager>();
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
            if (CurrentLevel < levels.Count)
            {
                MaxLevel = Math.Max(CurrentLevel, MaxLevel);
                InitLevel();
            }
            else
            {
                CurrentLevel = levels.Count - 1;
                throw new InvalidOperationException("GAME OVER");
            }

        }

        private void InitLevel()
        {
            // ReSharper disable once BuiltInTypeReferenceStyle
            switch (levels[CurrentLevel].ToList().Select<String, int>(getLevel).Max())
            {
                case 1:
                    themeManager.SwitchTheme(themeManager.LightTheme);
                    break;
                case 2:
                    themeManager.SwitchTheme(themeManager.MediumTheme);
                    break;
                case 3:
                    themeManager.SwitchTheme(themeManager.DarkTheme);
                    break;
            }

            levelPrefabs = GetComponent<LevelBuilder>().FillBlueprint(levels[CurrentLevel]);
            PrepareLevel();
            Invoke("StartLevel", transitionTime);
        }

        private int getLevel(string blueprint)
        {
            if (blueprint == "NA") return 0;
            return int.Parse(blueprint[0].ToString());
        }

        public GameObject Provide(int x, int y)
        {
            var element = levelPrefabs[x, y];
            return element != null ? element.Prefab.transform.parent.gameObject : null;
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
                {
                    instance.Prefab.elevate(instance.CurrentElevation);
                    GetComponent<PistonGrid>().Activate();
                }
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
            return CurrentLevel < MaxLevel || Debug.isDebugBuild;
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
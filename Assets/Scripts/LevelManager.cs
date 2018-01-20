using System;
using System.Linq;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Base.Scripts.Grid
{
    [RequireComponent(typeof(LevelBuilder), typeof(ThemeManager))]
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private ThemeManager themeManager;
        [SerializeField] private LevelBuilder LevelBuilder;
        [SerializeField] private float levelTextTransitionTime = 2f;

        private SwitchGrid grid;
        private readonly Settings settings = new Settings();

        public event Action<int> OnLevel = levelId => { };

        private void Start()
        {
            InitNextLevel();
        }

        private void EndLevel()
        {
            grid.OnPuzzleSolved -= EndLevel;
            grid.Deactivate();

            settings.CurrentLevel++;
            Invoke("InitNextLevel", 1);
        }

        private void InitNextLevel()
        {
            if (settings.CurrentLevel < LevelBuilder.AvailableLevels)
            {
                settings.MaxLevel = Math.Max(settings.CurrentLevel, settings.MaxLevel);
                InitLevel();
            }
            else
            {
                settings.CurrentLevel = LevelBuilder.AvailableLevels - 1;
                throw new InvalidOperationException("GAME OVER");
            }

        }

        private void InitLevel()
        {
            if(grid != null) Destroy(grid.gameObject);
            grid = LevelBuilder.CreateGrid(settings.CurrentLevel);
            grid.OnPuzzleSolved += EndLevel;

            switch (grid.MaxElevation)
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

            OnLevel(settings.CurrentLevel);
            Invoke("StartLevel", levelTextTransitionTime);
        }

        private void StartLevel()
        {
            grid.Activate();
        }

        public void NextLevel()
        {
            if (CanNextLevel())
                SetLevel(settings.CurrentLevel + 1);
        }

        public bool CanNextLevel()
        {
            return settings.CurrentLevel < settings.MaxLevel || (Application.isEditor && settings.CurrentLevel < LevelBuilder.AvailableLevels - 1);
        }

        public void RestartLevel()
        {
            SetLevel(settings.CurrentLevel);
        }

        public void PreviousLevel()
        {
            if(CanPreviousLevel())
            SetLevel(settings.CurrentLevel - 1);
        }

        public bool CanPreviousLevel()
        {
            return settings.CurrentLevel > 0;
        }

        private void SetLevel(int level)
        {
            CancelInvoke();
            settings.CurrentLevel = level;
            InitLevel();
        }
    }
}
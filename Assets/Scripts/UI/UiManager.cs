using Assets.Base.Scripts.Grid;
using Assets.Core.Extensions;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Transform iconGroup;
        [SerializeField] private IconButton iconButtonPrefab;
        [SerializeField] private LevelText levelText;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private float levelTextTransitionTime = 2f;

        private void Awake()
        {
            levelManager.OnLevel += id =>
            {
                levelText.TriggerLevel((id + 1).ToRoman(), levelTextTransitionTime);
            };

            var toggleSound = iconGroup.InstantiateChild(iconButtonPrefab);
            toggleSound.OnClick += () =>
            {
                if (AudioListener.volume == 1)
                {
                    AudioListener.volume = 0;
                    toggleSound.Icon = GoogleIcons.volume_up;
                }
                else
                {
                    AudioListener.volume = 1;
                    toggleSound.Icon = GoogleIcons.volume_off;
                }
            };
            toggleSound.Icon = GoogleIcons.volume_off;

            var refresh = iconGroup.InstantiateChild(iconButtonPrefab);
            refresh.OnClick += () =>
            {
                levelManager.RestartLevel();
            };
            refresh.Icon = GoogleIcons.refresh;

            var prevLevel = iconGroup.InstantiateChild(iconButtonPrefab);
            var nextLevel = iconGroup.InstantiateChild(iconButtonPrefab);

            prevLevel.OnClick += () =>
            {
                levelManager.PreviousLevel();
                prevLevel.Enabled = levelManager.CanPreviousLevel();
                nextLevel.Enabled = levelManager.CanNextLevel();
            };
            nextLevel.OnClick += () =>
            {
                levelManager.NextLevel();
                nextLevel.Enabled = levelManager.CanNextLevel();
                prevLevel.Enabled = levelManager.CanPreviousLevel();
            };

            prevLevel.Enabled = levelManager.CanPreviousLevel();
            prevLevel.Icon = GoogleIcons.keyboard_arrow_left;
            nextLevel.Enabled = levelManager.CanNextLevel();
            nextLevel.Icon = GoogleIcons.keyboard_arrow_right;
        }
    }
}
using System.Linq;
using Assets.Base.Scripts.Grid;
using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class PistonGrid : MonoBehaviour
    {
        public delegate void OnPuzzleSolvedHandler();
        public event OnPuzzleSolvedHandler OnPuzzleSolved;
        public AudioSource DragSound;

        private Grid grid;

        public int Range;

        public void Init()
        {
            grid = GetComponent<Grid>();
            
            transform.Children()
                .Select(child => child.GetComponent<Switch>())
                .ForEach(child => child.OnSwitchClicked += FlipSwitch);
        }

        public void DeInit()
        {
            transform.Children()
                .Select(child => child.GetComponent<Switch>())
                .ForEach(child => child.OnSwitchClicked -= FlipSwitch);
        }

        private void FlipSwitch(Switch clickableSwitch)
        {
            PlaySound();

            var cell = clickableSwitch.GetComponent<GridCell>();
            for (var x = cell.X - Range; x <= cell.X + Range; x++)
            {
                for (var y = cell.Y - Range; y <= cell.Y + Range; y++)
                {
                    if (grid.InBounds(x, y))
                    {
                        var neighbour = grid[x, y];
                        if (neighbour != null)
                            neighbour.GetComponent<Switch>().FlipSwitch();
                    }
                }
            }

            if (Solved() && OnPuzzleSolved != null) OnPuzzleSolved();
        }

        private void PlaySound()
        {
            DragSound.volume = Random.Range(0.8f, 1f);
            DragSound.Play();
        }

        private bool Solved()
        {
            return transform.Children().All(child => child.GetComponent<Switch>().IsDown());
        }

        public void Activate()
        {
            PlaySound();
        }
    }
}
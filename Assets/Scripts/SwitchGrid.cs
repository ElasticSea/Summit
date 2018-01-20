using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Base.Scripts;
using UnityEngine;
using Vexe.Runtime.Extensions;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class SwitchGrid : MonoBehaviour , IGridProvider<Switch>
    {
        public event Action OnPuzzleSolved = () => { };
        public AudioSource DragSound;

        public Grid<Switch> grid;

        public int Range;
        private Dictionary<Switch, int> elevations;
        public int MaxElevation => elevations.Keys.Max(s => s.Levels);

        public Switch Provide(int x, int y) => grid[x, y].Value;

        public void Init(Grid<Switch> grid)
        {
            elevations = grid.cells
                .Where(cell => cell.Value != null)
                .ToDictionary(cell => cell.Value, cell => cell.Value.Elevation);

            grid.cells
                .Where(cell => cell.Value != null)
                .ForEach(c => c.Value.Elevation = 0);

            this.grid = grid;
        }

        private Action<Switch> ValueOnOnSwitchClicked(Grid<Switch>.Cell cell)
        {
            return d => FlipSwitch(cell);
        }

        public void Activate()
        {
            grid.cells
                .Where(cell => cell.Value != null)
                .ForEach(c =>
                {
                    c.Value.AnimateElevation(elevations[c.Value]);
                    c.Value.OnSwitchClicked += ValueOnOnSwitchClicked(c);
                });

            PlaySound();
        }

        public void Deactivate()
        {
            grid.cells
                .Where(cell => cell.Value != null)
                .ForEach(c =>
                {
                    c.Value.AnimateElevation(0);
                    c.Value.OnSwitchClicked -= ValueOnOnSwitchClicked(c);
                });
        }

        private void FlipSwitch(Grid<Switch>.Cell cell)
        {
            PlaySound();

            for (var x = cell.X - Range; x <= cell.X + Range; x++)
            {
                for (var y = cell.Y - Range; y <= cell.Y + Range; y++)
                {
                    if (grid.InBounds(x, y))
                    {
                        grid[x, y].Value?.FlipSwitch();
                    }
                }
            }

            if (IsSolved()) OnPuzzleSolved.Invoke();
        }

        private void PlaySound()
        {
            DragSound.volume = Random.Range(0.5f, 1f);
            DragSound.pitch = Random.Range(0.85f, 1.15f);
            DragSound.Play();
        }

        private bool IsSolved() => grid.cells
            .Where(cell => cell.Value != null)
            .All(cell => cell.Value.IsDown());
        
    }
}
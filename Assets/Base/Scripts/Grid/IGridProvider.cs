using UnityEngine;

namespace Assets.Base.Scripts
{
    public interface IGridProvider
    {
        int Width { get; }
        int Height { get; }
        GameObject Provide(int x, int y);
    }
}
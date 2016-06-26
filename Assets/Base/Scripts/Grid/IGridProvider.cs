using UnityEngine;

namespace Assets.Base.Scripts
{
    public interface IGridProvider
    {
        GameObject Provide(int x, int y);
    }
}
using UnityEngine;

namespace Assets.Base.Scripts.Grid
{
    public class SingleTypeGridProvider : MonoBehaviour, IGridProvider
    {
        public GameObject Prefab;

        public GameObject Provide(int x, int y)
        {
            var instance = Instantiate(Prefab);
            instance.name += " [" + x + ", " + y + "]";
            return instance;
        }
    }
}
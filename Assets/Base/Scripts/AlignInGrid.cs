using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Base.Scripts
{
    public class AlignInGrid : MonoBehaviour
    {
        public GameObject Prefab;
        public int Width;
        public int Height;
        public float ItemOffset;

        public void Align()
        {
            transform.RemoveAllChildren();
            var size = 1 + ItemOffset;
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var instance = Instantiate(Prefab);
                    instance.transform.SetParent(transform);
                    instance.name += " " + x + ":" + y;
                    var xpos = (x - (Width - 1) / 2f) * size;
                    var ypos = (y - (Height - 1) / 2f) * size;
                    instance.transform.position = new Vector2(xpos, ypos).ToXZ();
                }
            }
        }
    }
}
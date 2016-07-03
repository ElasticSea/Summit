using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ThemeManager : MonoBehaviour
    {
        public Material BlockMaterial;
        public Theme LightTheme;
        public Theme DarkTheme;
        public Theme MediumTheme;

        public void SwitchTheme(Theme theme)
        {
            RenderSettings.fogColor = theme.Fog;
            BlockMaterial.mainTexture = theme.Texture;
        }

        [Serializable]
        public class Theme
        {
            public Color Fog;
            public Texture Texture;
        }
    }
}
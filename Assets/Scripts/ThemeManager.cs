using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class ThemeManager : MonoBehaviour
    {
        public Material BlockMaterial;
        public Theme LightTheme;
        public Theme DarkTheme;
        public Theme MediumTheme;

        private Theme last;
        private Theme current;
        private float value;

        public void SwitchTheme(Theme theme)
        {
            if (last == null) last = theme;

            current = theme;
            BlockMaterial.SetTexture("_MainTex", last.Texture);
            BlockMaterial.SetTexture("_Secondary", current.Texture);
            Value = 0;

            DOTween.To(
                () => Value,
                v => Value = v,
                1,
                .4f
            );

        }

        public float Value
        {
            get { return value; }
            set
            {
                this.value = value;

                var currentColor = Color.Lerp(last.Fog, current.Fog, value);
                RenderSettings.fogColor = currentColor;
                Camera.main.backgroundColor = currentColor;
                BlockMaterial.SetFloat("_Blend", value);

                if (value == 1)
                {
                    last = current;
                }
            }
        }

        [Serializable]
        public class Theme
        {
            public Color Fog;
            public Texture Texture;
        }
    }
}
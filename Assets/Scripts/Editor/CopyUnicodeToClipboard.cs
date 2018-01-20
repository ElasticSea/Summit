using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    public class CopyUnicodeToClipboard : EditorWindow
    {
        private string text = "";

        [MenuItem("Window/Copy Unicode To Clipboard")]
        static void show()
        {
            CopyUnicodeToClipboard window = GetWindow<CopyUnicodeToClipboard>(false, "Copy Unicode To Clipboard");
            window.Show();
        }

        void OnGUI()
        {
            text = EditorGUILayout.TextField(text);
            if (GUILayout.Button("Copy to Clipboard"))
            {
                var number = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
                EditorGUIUtility.systemCopyBuffer = Char.ToString((char)number);
            }
        }

        void OnInspectorUpdate()
        {
            this.Repaint();
        }
    }
}
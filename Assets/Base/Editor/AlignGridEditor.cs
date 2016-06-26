using Assets.Base.Scripts;
using Assets.Base.Scripts.Grid;
using UnityEditor;
using UnityEngine;

namespace Assets.Base.Editor
{
    [CustomEditor(typeof(AlignInGrid))]
    public class AlignGridEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Align"))
                (target as AlignInGrid).Align();
        }
    }
}
using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AtlasReplacementData))]
public class AtlasReplacementDataInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginVertical();

        if (GUILayout.Button("Open Editor"))
        {
            AtlasReplacer.ShowWindow();
            AtlasReplacer.Instance.SetAtlasReplacementData(Selection.activeObject);
        }

        GUILayout.EndVertical();
    }
}
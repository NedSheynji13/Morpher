using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Consolecreator))]
public class ConsolecreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Consolecreator Script = (Consolecreator)target;
        if (GUILayout.Button("CreateLight"))
            Script.BuildLight();
        if (GUILayout.Button("CreateHeavy"))
            Script.BuildHeavy();
        if (GUILayout.Button("Delete"))
            Script.Delete();
        if (GUI.changed)
            EditorUtility.SetDirty(Script);
    }
}
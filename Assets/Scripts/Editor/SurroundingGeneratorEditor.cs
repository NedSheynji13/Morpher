using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SurroundingGenerator))]
public class urroundingGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SurroundingGenerator Script = (SurroundingGenerator)target;
        if (GUILayout.Button("CreateWall"))
            Script.BuildWall();
        if (GUILayout.Button("CreateFloor"))
            Script.BuildFloor();
        if (GUILayout.Button("Delete"))
            Script.Delete();
        if (GUILayout.Button("HideInHierarchy"))
            Script.HideWallparts();
        if (GUILayout.Button("ShowInHierarchy"))
            Script.ShowWallParts();
    }
}
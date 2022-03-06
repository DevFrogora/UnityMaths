using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(LevelScript))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelScript myTarget = (LevelScript)target;

        myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        EditorGUILayout.LabelField("Level", myTarget.Level.ToString());

        // to make inspector as what it is ,use DrawDefaultInspector();

        EditorGUILayout.HelpBox("1, this Script inspector Manipulated by LevelScriptEditor ", MessageType.Info);
    }
}
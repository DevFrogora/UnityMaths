using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyFirstEditorWindow : EditorWindow
{
  
    [MenuItem("Tools/MyFirstWindow")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyFirstEditorWindow));
    }

    public static EditorWindow RefToWindow()
    {
       return EditorWindow.GetWindow(typeof(MyFirstEditorWindow));
    }

    public MyFirstEditorWindow()
    {
        this.titleContent = new GUIContent("My Window");
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(50);
        GUILayout.Label("My First Editor Window");
        GUILayout.Space(50);

        if(GUILayout.Button("Test Button"))
        {
            TestFunction();
        }
        GUILayout.EndVertical();
    }

    void TestFunction()
    {
        Debug.Log("Test");
    }
}

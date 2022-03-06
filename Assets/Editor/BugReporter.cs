using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BugReporter : EditorWindow
{
    string bugReportName = "";
    string description = "";
    GameObject buggyGameObject;

    [MenuItem("Tools/BugReporter")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BugReporter));
    }

    public static EditorWindow RefToWindow()
    {
        return EditorWindow.GetWindow(typeof(BugReporter));
    }

    public BugReporter()
    {
        this.titleContent = new GUIContent("Bug Reporter");
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 20;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Bug Reporter");
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;

        GUILayout.Space(10);
        bugReportName = EditorGUILayout.TextField("Bug Name", bugReportName);
        GUILayout.Space(10);
        GUILayout.Label("Scene : " + EditorApplication.currentScene);
        GUILayout.Space(10);
        GUILayout.Label("Time : " + System.DateTime.Now);

        GUILayout.Space(10);
        buggyGameObject = (GameObject)EditorGUILayout.ObjectField("Buggy Game object", buggyGameObject, typeof(GameObject), true);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Description",GUILayout.MaxWidth(90));
        description = EditorGUILayout.TextArea( description, GUILayout.MaxHeight(75));
        GUILayout.EndHorizontal();

        if(GUILayout.Button("Save Bug"))
        {
            SaveBug();
        }

        if (GUILayout.Button("Save Bug With Screenshot"))
        {
            SaveBugWithScreenshot();
        }

        GUILayout.EndVertical();
    }

    void SaveBug()
    {
        Directory.CreateDirectory("Assets/BugReports/" + bugReportName);
        StreamWriter sw = new StreamWriter("Assets/BugReports/" + bugReportName + "/" + bugReportName + ".txt");
        sw.WriteLine(bugReportName);
        sw.WriteLine(System.DateTime.Now.ToString());
        sw.WriteLine(EditorApplication.currentScene);
        sw.WriteLine(description);
        sw.Close();
        UnityEditor.AssetDatabase.Refresh();
    }

    void SaveBugWithScreenshot()
    {
        Directory.CreateDirectory("Assets/BugReports/" + bugReportName);
        StreamWriter sw = new StreamWriter("Assets/BugReports/" + bugReportName + "/" + bugReportName + ".txt");
        sw.WriteLine(bugReportName);
        sw.WriteLine(System.DateTime.Now.ToString());
        sw.WriteLine(EditorApplication.currentScene);
        sw.WriteLine(description);
        sw.Close();

        ScreenCapture.CaptureScreenshot("Assets/BugReports/" + bugReportName + "/" + bugReportName + "_scrrenshot.png");
        UnityEditor.AssetDatabase.Refresh();
    }

}
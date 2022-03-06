using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AI))]
public class AIInspector : Editor
{
    AI ai;

    private void OnEnable()
    {
        ai = (AI)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

    }

    private void OnSceneGUI()
    {
        Handles.Label(ai.transform.position + new Vector3(0,5,1), "Deadly AI : " + ai.name);
        ai.areaRadius = Handles.RadiusHandle(Quaternion.identity, ai.transform.position, ai.areaRadius);
        ai.speed = Handles.ScaleValueHandle(ai.speed, ai.transform.position, Quaternion.identity, ai.speed, Handles.ArrowHandleCap, 0.5f);


        if(ai.showNodeHandles)
        {
            Handles.color = Color.blue;
            for (int i = 0; i < ai.nodePoints.Length; i++)
            {
                ai.nodePoints[i] = Handles.PositionHandle(ai.nodePoints[i], ai.nodePointsRoatation[i]);
                Handles.DrawLine(ai.nodePoints[i], ai.nodePoints[(int)Mathf.Repeat(i + 1, ai.nodePoints.Length)]);

            }

            Handles.color = Color.white;
        }

        if(ai.showNodeLines)
        {
            Handles.color = Color.blue;
            for (int i = 0; i < ai.nodePoints.Length; i++)
            {
                Handles.DrawLine(ai.nodePoints[i], ai.nodePoints[(int)Mathf.Repeat(i + 1, ai.nodePoints.Length)]);
            }
            Handles.color = Color.white;
        }

        if(ai.showNodeHandlesRotation)
        {
            for (int i = 0; i < ai.nodePointsRoatation.Length; i++)
            {
                ai.nodePointsRoatation[i] = Handles.RotationHandle(ai.nodePointsRoatation[i], ai.nodePoints[i]);
            }
        }

        Handles.BeginGUI();
        GUILayout.BeginVertical();

        GUILayout.BeginArea(new Rect(10,10,80,500));
        GUILayout.Label("Lobby Area");
        if(GUILayout.Button("Player Info",GUILayout.MinHeight(20)))
        {

        }
        if (GUILayout.Button("Jump", GUILayout.MinHeight(20)))
        {

        }
        if (GUILayout.Button("Fire", GUILayout.MinHeight(20)))
        {

        }
        if (GUILayout.Button("Die", GUILayout.MinHeight(20)))
        {

        }

        GUILayout.EndArea();

        GUILayout.EndVertical();
        Handles.EndGUI();

    }
}

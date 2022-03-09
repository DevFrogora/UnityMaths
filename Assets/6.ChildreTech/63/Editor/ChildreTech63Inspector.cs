using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// To delink the editor script comment the custom Editor header
[CustomEditor(typeof(ChildreTech63))]
public class ChildreTech63Inspector : Editor
{
    ChildreTech63 cube;
    void OnEnable()
    {
        cube = (ChildreTech63)(target);

    }
    int currentIndex;
    int degree;
    //float  shiftGraph;
    public override void OnInspectorGUI()
    {

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("calculate distance"))
        {
            Debug.Log("distance :  "+GetLength(0,0,cube.transform.position.z, cube.transform.position.y));
            
        }
        if (GUILayout.Button("calculate Angle"))
        {
            Debug.Log("Angle  :  " + GetAngle(0, 0, cube.transform.position.z, cube.transform.position.y));

        }

        EditorGUILayout.EndHorizontal();
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.blue;
        Handles.DrawLine(new Vector3(0,0 ,0) , new Vector3(0, cube.transform.position.y, cube.transform.position.z));  // for circular motion
    }


    float GetLength(float x1, float y1 , float x2, float y2)
    {
        float x = x2 - x1;
        float y = y2 - y1;

        float h = Mathf.Sqrt((x * x) + (y * y));


        return Mathf.Abs(h);
    }

    float GetAngle(float x1, float y1, float x2, float y2)
    {
        float x = x2 - x1;
        float y = y2 - y1;

        float angle = Mathf.Atan2(y,x);


        return angle * Mathf.Rad2Deg;
    }
}

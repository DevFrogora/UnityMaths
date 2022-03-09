using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// To delink the editor script comment the custom Editor header
[CustomEditor(typeof(ChildreTech64))]
public class ChildreTech64Inspector : Editor
{
    ChildreTech64 cube;
    void OnEnable()
    {
        cube = (ChildreTech64)(target);

    }
    int currentIndex;
    int degree;
    //float  shiftGraph;
    float angleInRad;
    float lengthOfHypto;
    public override void OnInspectorGUI()
    {

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("calculate distance"))
        {
            Debug.Log("distance :  " + GetLength(0, 0, cube.transform.position.z, cube.transform.position.y));

        }

        if (GUILayout.Button("calculate Angle"))
        {
            
            Debug.Log("Angle  :  " + GetAngleInDeg(0, 0, cube.transform.position.z, cube.transform.position.y));

        }


        if (GUILayout.Button("Get X"))
        {
            lengthOfHypto= GetLength(0, 0, cube.transform.position.z, cube.transform.position.y);
            angleInRad = GetAngleInRad(0, 0, cube.transform.position.z, cube.transform.position.y);

            // we know magnitude(length or distance )  and angle and want to find x value
            Debug.Log("X  :  " + GetXDis(angleInRad, lengthOfHypto));

        }
        if (GUILayout.Button("Get Y"))
        {
            lengthOfHypto = GetLength(0, 0, cube.transform.position.z, cube.transform.position.y);
            angleInRad = GetAngleInRad(0, 0, cube.transform.position.z, cube.transform.position.y);

            // we know magnitude(length or distance )  and angle and want to find Y value
            Debug.Log("Y  :  " + GetYDis(angleInRad, lengthOfHypto));
        }

        EditorGUILayout.EndHorizontal();
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.blue;
        Handles.DrawLine(new Vector3(0, 0, 0), new Vector3(0, cube.transform.position.y, cube.transform.position.z));  // for circular motion
    }


    float GetLength(float x1, float y1, float x2, float y2)
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

        float angle = Mathf.Atan2(y, x);


        return angle * Mathf.Rad2Deg;
    }

    float GetAngleInRad(float x1, float y1, float x2, float y2)
    {
        float x = x2 - x1;
        float y = y2 - y1;

        return  Mathf.Atan2(y, x);


    }

    float GetAngleInDeg(float x1, float y1, float x2, float y2)
    {
        float x = x2 - x1;
        float y = y2 - y1;

        return ((Mathf.Atan2(y, x) * Mathf.Rad2Deg) + 360.0f) % 360.0f; // added 360 as atan2 return 0-180 and o to -180 and % modulas 360 so get the remainder if it greater then 360
    }

    float GetXDis(float angleInRad, float length)
    {
        return Mathf.Cos(angleInRad) * length;
    }
    float GetYDis(float angleInRad, float length)
    {
        return Mathf.Sin(angleInRad) * length;
    }
}

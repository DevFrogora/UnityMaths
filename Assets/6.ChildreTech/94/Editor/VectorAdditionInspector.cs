using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VectorAddition))]
public class VectorAdditionInspector : Editor
{
    VectorAddition cube;

    void OnEnable()
    {
        cube = (VectorAddition)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Calc Magnitude"))
        {
            Debug.Log(cube.gameObject.transform.position.magnitude);
        }
    }
    float degree;
    private void OnSceneGUI()
    {
        Handles.color = Color.blue;
        Handles.DrawLine(Vector3.zero, cube.transform.position);
        Handles.Label(cube.transform.position + (2 *Vector3.one), "Distance from origin : " + cube.gameObject.transform.position.magnitude);
        degree = GetAngleInDeg(0, 0, cube.transform.position.z, cube.transform.position.y);
        Handles.Label(Vector3.zero - (3 * Vector3.one), "angle : " + degree);
        Handles.color = Color.red;
        var center = Vector3.zero;
        var start =  Vector3.forward;
        var normal = Vector3.left;
        var radius = 1;
        var angle = degree;


        /// <summary>
        /// when we add one vector to another vector , then the resultant vector will be move  forward 
        /// </summary>
        cube.capsule.transform.position = AddVector(cube.transform.position,
                                            cube.anotherVector.transform.position);

        /// <summary>
        /// when we sub  one vector to another vector , then the resultant vector will be move backward 
        /// </summary>

        //cube.capsule.transform.position = SubVector(cube.transform.position,
        //                                    cube.anotherVector.transform.position);





        Handles.Label(cube.capsule.transform.position + (3 * Vector3.one), "resultant Length : " + cube.capsule.transform.position.magnitude);
        Handles.DrawLine(Vector3.zero, cube.capsule.transform.position);

        Handles.DrawLine(Vector3.zero, 3* Vector3.forward);
        Handles.color = Color.yellow;
        Handles.DrawWireArc(center, normal, start, angle, radius, 1);
    }

    float GetAngleInDeg(float x1, float y1, float x2, float y2)
    {
        float x = x2 - x1;
        float y = y2 - y1;

        return ((Mathf.Atan2(y, x) * Mathf.Rad2Deg) + 360.0f) % 360.0f; // added 360 as atan2 return 0-180 and o to -180 and % modulas 360 so get the remainder if it greater then 360
    }

    Vector3 AddVector(Vector3 a , Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    Vector3 SubVector(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }
}

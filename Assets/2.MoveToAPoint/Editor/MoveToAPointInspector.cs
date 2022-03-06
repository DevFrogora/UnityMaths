using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// To delink the editor script comment the custom Editor header
//[CustomEditor(typeof(MoveToAPoint))]
public class MoveToAPointInspector : Editor
{

    MoveToAPoint cube;
    public bool isReachedB = false;



    private void OnEnable()
    {
        cube = (MoveToAPoint)target;
        cube.gameObject.transform.position = cube.A.position;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("we will move forward in Z direction");
        isReachedB = EditorGUILayout.Toggle("isReachedB ", isReachedB);
        cube.A = (Transform)EditorGUILayout.ObjectField("Point A", cube.A, typeof(Transform), true);
        cube.B = (Transform)EditorGUILayout.ObjectField("Point B", cube.B, typeof(Transform), true);
        cube.C = (Transform)EditorGUILayout.ObjectField("Point C", cube.C, typeof(Transform), true);
        //cube.Points = (Transform )EditorGUILayout.ObjectField("Points Array", cube.Points);

        cube.speed = EditorGUILayout.Slider("Speed", cube.speed, -20, 20);
        EditorGUILayout.BeginHorizontal();

        if(!isReachedB)
        cube.gameObject.transform.position = Vector3.MoveTowards(cube.gameObject.transform.position, cube.B.position, cube.speed * Time.deltaTime);

        if(cube.gameObject.transform.position == cube.B.position)
        {
            isReachedB = true;
        }
        if(isReachedB)
        {
            cube.gameObject.transform.position = Vector3.MoveTowards(cube.gameObject.transform.position, cube.C.position, cube.speed * Time.deltaTime);
            Debug.Log("move to C");

        }

        if (GUILayout.Button("Reset"))
        {
            //float step = cube.speed * Time.deltaTime;
            cube.gameObject.transform.position = cube.A.position;
            cube.speed = 0;
            isReachedB = false;
        }

        EditorGUILayout.EndHorizontal();


        EditorGUILayout.EndVertical();


    }

}

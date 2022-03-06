using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// To delink the editor script comment the custom Editor header
[CustomEditor(typeof(MoveObjectForward))]
public class MoveObjectForwardInspector : Editor
{
    MoveObjectForward cube;


    private void OnEnable()
    {
        cube = (MoveObjectForward)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("we will move forward in Z direction");


        cube.speed = EditorGUILayout.Slider("Speed", cube.speed, -20, 20);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Move +Y"))
        {
            cube.gameObject.transform.position = cube.gameObject.transform.position + cube.gameObject.transform.up;
        }

        if (GUILayout.Button("Move +X"))
        {
            cube.gameObject.transform.position = cube.gameObject.transform.position + cube.gameObject.transform.right;
        }

        if (GUILayout.Button("Move -X"))
        {
            cube.gameObject.transform.position = cube.gameObject.transform.position - cube.gameObject.transform.right;
        }

        if (GUILayout.Button("Move -Y"))
        {
            cube.gameObject.transform.position = cube.gameObject.transform.position - cube.gameObject.transform.up;
        }
        if(cube.speed >0)
        {
            cube.gameObject.transform.position = cube.gameObject.transform.position + cube.gameObject.transform.forward * Time.deltaTime * cube.speed;
            Debug.Log("Moving +z");
        }
        if (cube.speed < 0)
        {
            cube.gameObject.transform.position = cube.gameObject.transform.position - cube.gameObject.transform.forward * Time.deltaTime * (-cube.speed);
            Debug.Log("Moving -z");
        }

        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Stop and reset"))
        {
            cube.gameObject.transform.position = new Vector3(0, 0, 0);
            cube.speed = 0;
        }
        EditorGUILayout.BeginHorizontal();


        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();


    }
}

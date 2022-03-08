using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
// To delink the editor script comment the custom Editor header
[CustomEditor(typeof(MoveCircular))]
public class MoveCircularInspector : Editor
{

    MoveCircular cube;
    int totalRotation;
    void OnEnable()
    {
        cube = (MoveCircular)(target);

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Circular Motion ");


        //cube.speed = EditorGUILayout.Slider("increase for delay", (2 * Mathf.PI) / cube.speed, 0, 20);
        //cube.angle = EditorGUILayout.Slider("increase for delay", cube.angle, 0, 360);
        cube.timeCounter = EditorGUILayout.Slider("time Counter", cube.timeCounter, 0, 1000);
        cube.x = EditorGUILayout.Slider("x", cube.x, 0, 20);
        cube.y = EditorGUILayout.FloatField("y ", cube.y);
        cube.z = EditorGUILayout.FloatField("z ", cube.z);
        cube.speed = EditorGUILayout.Slider("speed ",  cube.speed,0,20);
        EditorGUILayout.HelpBox("Increase speed to make cube move",MessageType.Info);


        cube.width = EditorGUILayout.Slider("width", cube.width, 0, 20);
        cube.height = EditorGUILayout.Slider("height ", cube.height, 0, 20);

        cube.timeCounter += Time.deltaTime*cube.speed;
        cube.x = 0;
        cube.y = Mathf.Cos(cube.timeCounter) * cube.height;
        cube.z = Mathf.Sin(cube.timeCounter) * cube.width;

        int NumbeOFRotation = (int)(cube.timeCounter * Mathf.Rad2Deg / 360f);
        
        if (NumbeOFRotation == 1)
        {
            Debug.Log(cube.timeCounter);
            cube.timeCounter = 0;
            totalRotation++;
        }
        EditorGUILayout.IntField("Complete Rotation", totalRotation);


        cube.gameObject.transform.position = new Vector3(cube.x, cube.y, cube.z);
        
        EditorGUILayout.BeginHorizontal();

        #region Reset
        if (GUILayout.Button("Reset"))
        {
            cube.speed = 0;
            cube.timeCounter = 0;
            totalRotation = 0;
           
        }
        #endregion
        EditorGUILayout.EndHorizontal();


        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndVertical();
    }

    IEnumerator moveTheObject(int goToIndex)
    {

        yield return null;
    }
}

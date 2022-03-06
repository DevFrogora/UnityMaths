using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
// To delink the editor script comment the custom Editor header
[CustomEditor(typeof(MoveToAPoint))]
public class MoveToPointInspector2 : Editor
{
    MoveToAPoint cube;
    SerializedProperty Points;
    bool isReached = false;
    int goToIndex=0;

    void OnEnable()
    {
        cube = (MoveToAPoint)(target);
        cube.gameObject.transform.position = cube.Points[goToIndex].transform.position;
         goToIndex = 0;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("we will move forward in Z direction");
        goToIndex = EditorGUILayout.IntField("goToIndex ", goToIndex);

        Points = serializedObject.FindProperty("Points");
        EditorGUILayout.PropertyField(Points, new GUIContent("Points"), true);

        cube.speed = EditorGUILayout.Slider("Speed", cube.speed, -20, 20);
        #region Comments
        //Debug.Log("A : "+cube.Points[0].transform.position);
        //Debug.Log("B : "+cube.Points[1].transform.position);
        //Debug.Log("C : "+cube.Points[2].transform.position);

        //if (!isReachedB)
        //    cube.gameObject.transform.position = Vector3.MoveTowards(cube.gameObject.transform.position, cube.Points[1].transform.position, cube.speed * Time.deltaTime);

        //if (cube.gameObject.transform.position == cube.B.position)
        //{
        //    isReachedB = true;
        //}
        //if (isReachedB)
        //{
        //    cube.gameObject.transform.position = Vector3.MoveTowards(cube.gameObject.transform.position, cube.Points[2].transform.position, cube.speed * Time.deltaTime);
        //    Debug.Log("move to C");
        //}
        #endregion

        EditorGUILayout.BeginHorizontal();

        #region Reset
        if (GUILayout.Button("Reset"))
        {
            //float step = cube.speed * Time.deltaTime;
            cube.gameObject.transform.position = cube.Points[0].transform.position;
            cube.speed = 0;
            goToIndex = 0;
        }
        #endregion
        EditorGUILayout.EndHorizontal();

        if(goToIndex <= cube.Points.Length -1)
        {

            if (cube.gameObject.transform.position == cube.Points[goToIndex].transform.position) { 

                isReached = true;
                Debug.Log(goToIndex);
            }

            EditorCoroutineUtility.StartCoroutine(moveTheObject(goToIndex), this);
        }


        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndVertical();
    }

    IEnumerator moveTheObject(int goToIndex)
    {
        cube.gameObject.transform.position = Vector3.MoveTowards(cube.gameObject.transform.position, cube.Points[goToIndex].transform.position, cube.speed * Time.deltaTime);
        if (isReached)
        {
            this.goToIndex++;
            isReached = false;
        }
        yield return null;
    }


}

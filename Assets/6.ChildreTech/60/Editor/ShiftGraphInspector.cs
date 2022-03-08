using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// To delink the editor script comment the custom Editor header
[CustomEditor(typeof(ShiftGraph))]
public class ShiftGraphInspector : Editor
{
    ShiftGraph cube;
    float[] angle = { 0, Mathf.PI / 2, Mathf.PI, 3 * Mathf.PI / 2, 2 * Mathf.PI };
    float[] y = { 0, 0, 0, 0, 0 };
    float speed;
    float timeCounter;
    void OnEnable()
    {
        cube = (ShiftGraph)(target);

    }
    int currentIndex;
    int degree;
    float shiftGraph;

    public enum ShiftDirection
    {
        None,
        Zdirection,
        YDirection,
    };
    ShiftDirection shiftGraphDirection;
    public override void OnInspectorGUI()
    {
        timeCounter += speed * Time.deltaTime;
        y[currentIndex] = MySin(angle[currentIndex]);

        #region linearMoveCircularMotion
        // uncomment the if statement to get it work and comment the curveMove region
        //cube.transform.position = Vector3.MoveTowards(cube.transform.position, new Vector3(0, y[currentIndex], Mathf.Cos(angle[currentIndex])), speed * Time.deltaTime);
        speed = EditorGUILayout.Slider("speed", speed, 0, 10);
        EditorGUILayout.FloatField("CurrentIndex", currentIndex);
        //if (cube.transform.position == new Vector3(0, y[currentIndex], Mathf.Cos(angle[currentIndex])))
        //{
        //    if(currentIndex < angle.Length -1)
        //    currentIndex++;
        //}
        #endregion


        #region curveMove 
        // can use multiply width and height to expand the radius of the circle with y and z
        if (shiftGraphDirection == ShiftDirection.None)
        {
            cube.gameObject.transform.position = new Vector3(0, Mathf.Sin(timeCounter), Mathf.Cos(timeCounter));
        }
  
        if (shiftGraphDirection == ShiftDirection.YDirection)
        {
            cube.gameObject.transform.position = new Vector3(0, Mathf.Sin(timeCounter) + shiftGraph, Mathf.Cos(timeCounter));
        }
        if (shiftGraphDirection == ShiftDirection.Zdirection)
        {
            cube.gameObject.transform.position = new Vector3(0, Mathf.Sin(timeCounter) , Mathf.Cos(timeCounter) + shiftGraph);
        }
            degree = (int)(timeCounter * Mathf.Rad2Deg);
        if (degree % 90f == 0)
        {
            Debug.Log("WE hit  " + (int)degree);
            if (degree == 0)
            {
                currentIndex = 1;
            }
            if (degree == 90)
            {
                currentIndex = 2;
            }
            if (degree == 180)
            {
                currentIndex = 3;
            }
            if (degree == 270)
            {
                currentIndex = 4;
            }
            if (degree == 360)
            {
                timeCounter = 0;
                currentIndex = 0;
            }
        }

        #endregion

        shiftGraph = EditorGUILayout.Slider("shiftGraph", shiftGraph, -5, 5);
        if (GUILayout.Button("Reset"))
        {

            currentIndex = 0;
            speed = 0;
            cube.transform.position = new Vector3(0, y[0], Mathf.Cos(angle[currentIndex]));
            timeCounter = 0;
            shiftGraph = 0;
        }

        if (GUILayout.Button("Shift Y"))
        {
            shiftGraphDirection = ShiftDirection.YDirection;
        }
        if (GUILayout.Button("Shift Z"))
        {
            shiftGraphDirection = ShiftDirection.Zdirection;
        }

    }

    float MySin(float angle)
    {
        return Mathf.Sin(angle);
    }

    private void OnSceneGUI()
    {
        for (int i = 0; i < angle.Length - 1; i++)
        {
            if (shiftGraphDirection == ShiftDirection.Zdirection)
            {
                Handles.DrawLine(new Vector3(0, y[i], Mathf.Cos(angle[i]) + shiftGraph), new Vector3(0, y[i + 1], Mathf.Cos(angle[i + 1]) + shiftGraph)); // for shift graph
            }
            if (shiftGraphDirection == ShiftDirection.YDirection)
            {
                Handles.DrawLine(new Vector3(0, y[i] + shiftGraph, Mathf.Cos(angle[i])), new Vector3(0, y[i + 1] + shiftGraph, Mathf.Cos(angle[i + 1]))); // for shift graph
            }
            if (shiftGraphDirection == ShiftDirection.None)
            {
                Handles.DrawLine(new Vector3(0, y[i],  Mathf.Cos(angle[i])), new Vector3(0, y[i + 1] , Mathf.Cos(angle[i + 1]))); // for shift graph
            }
            // re assign y[i] and z[i] for saving the shift data
        }


    }
}

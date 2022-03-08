using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// To delink the editor script comment the custom Editor header
[CustomEditor(typeof(ChildreTech62))]
public class ChildreTech62Inspector : Editor
{
    ChildreTech62 cube;
    float[] angle = { 0, Mathf.PI / 2, Mathf.PI, 3 * Mathf.PI / 2, 2 * Mathf.PI };
    float[] y = { 0, 0, 0, 0, 0 };
    float speed;
    float timeCounter;
    void OnEnable()
    {
        cube = (ChildreTech62)(target);

    }
    int currentIndex;
    int degree;
    //float  shiftGraph;
    public override void OnInspectorGUI()
    {
        timeCounter += speed * Time.deltaTime;
        y[currentIndex] = MySin(angle[currentIndex]);

        speed = EditorGUILayout.Slider("speed", speed, 0, 10);
        EditorGUILayout.FloatField("CurrentIndex", currentIndex);



        #region curveMove 
        // can use multiply width and height to expand the radius of the circle with y and z
        cube.gameObject.transform.position = new Vector3(0, Mathf.Sin(timeCounter), Mathf.Cos(timeCounter));
        degree = (int)(timeCounter * Mathf.Rad2Deg);
        Debug.Log(Mathf.Atan2(cube.transform.position.y, cube.transform.position.z) * Mathf.Rad2Deg);
        if (degree % 90f == 0)
        {
            //Debug.Log("WE hit  " + (int)degree);
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
        if (GUILayout.Button("Reset"))
        {

            currentIndex = 0;
            speed = 0;
            cube.transform.position = new Vector3(0, y[0], Mathf.Cos(angle[currentIndex]));
            timeCounter = 0;
        }


    }

    float MySin(float angle)
    {
        return Mathf.Sin(angle);
    }

    private void OnSceneGUI()
    {
        for (int i = 0; i < angle.Length - 1; i++)
            Handles.DrawLine(new Vector3(0, y[i], Mathf.Cos(angle[i])), new Vector3(0, y[i + 1], Mathf.Cos(angle[i + 1])));  // for circular motion

    }
}

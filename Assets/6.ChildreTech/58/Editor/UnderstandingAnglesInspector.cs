using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
// To delink the editor script comment the custom Editor header
[CustomEditor(typeof(UnderstandingAngles))]
public class UnderstandingAnglesInspector : Editor
{
    UnderstandingAngles cube;
    float[] z = { -3, -2, -1, 0, 1, 2, 3 };
    float[] y = { 0,0,0,0,0,0,0 };
    float speed;


    void OnEnable()
    {
        cube = (UnderstandingAngles)(target);
        cube.gameObject.transform.position = new Vector3(0,
                   y[0], z[0]);
    }


    Vector3 directionA;
    int currentIndex;
    public enum FofX
    {
        None,
        Constant,
        X,
        Square,
    }

    FofX functionType;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(functionType == FofX.Constant)
        {
            y[currentIndex] = fofConstant(z[currentIndex]);
            cube.gameObject.transform.position = Vector3.MoveTowards(cube.gameObject.transform.position, new Vector3(0, y[currentIndex], z[currentIndex]), speed * Time.deltaTime);
            if (cube.gameObject.transform.position == new Vector3(0, y[currentIndex], z[currentIndex]))
            {
                if (currentIndex < z.Length - 1)
                {
                    currentIndex++;
                }
            }
        }

        if (functionType == FofX.X)
        {
            y[currentIndex] = fofX(z[currentIndex]);
            cube.gameObject.transform.position = Vector3.MoveTowards(cube.gameObject.transform.position, new Vector3(0, y[currentIndex], z[currentIndex]), speed * Time.deltaTime);
            if (cube.gameObject.transform.position == new Vector3(0, y[currentIndex], z[currentIndex]))
            {
                if (currentIndex < z.Length - 1)
                {
                    currentIndex++;
                }
            }
        }

        if (functionType == FofX.Square)
        {
            y[currentIndex] = fofSquare(z[currentIndex]);
            cube.gameObject.transform.position = Vector3.MoveTowards(cube.gameObject.transform.position, new Vector3(0, y[currentIndex], z[currentIndex]), speed * Time.deltaTime);
            if (cube.gameObject.transform.position == new Vector3(0, y[currentIndex], z[currentIndex]))
            {
                if (currentIndex < z.Length - 1)
                {
                    currentIndex++;
                }
            }
        }

        EditorGUILayout.FloatField("CurrentIndex", currentIndex);
        speed = EditorGUILayout.Slider("speed", speed,0,10);

        functionType = (FofX)EditorGUILayout.EnumPopup("Current Function ", functionType);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("F(x) = 2"))
        {
            functionType = FofX.Constant;
        }

        if (GUILayout.Button("F(x)= X"))
        {
            functionType = FofX.X;
        }

        if (GUILayout.Button("F(x)= square(x)"))
        {
            functionType = FofX.Square;
        }

        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Reset"))
        {
            if(functionType == FofX.Constant)
            {
                currentIndex = 0;
                for (int i = 0; i < y.Length; i++)
                {
                    y[i] = fofConstant(z[i]);
                }
                speed = 0;
                cube.gameObject.transform.position = new Vector3(0,
                           y[0], z[0]);
            }

            if (functionType == FofX.X)
            {
                currentIndex = 0;
                for (int i = 0; i < y.Length; i++)
                {
                    y[i] = fofX(z[i]);
                }
                speed = 0;
                cube.gameObject.transform.position = new Vector3(0,
                           y[0], z[0]);
            }

            if (functionType == FofX.Square)
            {
                currentIndex = 0;
                for (int i = 0; i < y.Length; i++)
                {
                    y[i] = fofSquare(z[i]);
                }
                speed = 0;
                cube.gameObject.transform.position = new Vector3(0,
                           y[0], z[0]);
            }

        }
        //EditorCoroutineUtility.StartCoroutine(moveTheObject(), this);

    }

    private void OnSceneGUI()
    {
        if (currentIndex <= z.Length - 1)
        {
            Handles.color = Color.yellow;
            if(currentIndex != 0)
            Handles.DrawLine(new Vector3(0, y[currentIndex], z[currentIndex - 1]), new Vector3(0, y[currentIndex], z[currentIndex]));
        }

    }

    float fofX(float z)
    {
        return z;
    }

    float fofConstant(float z)
    {
        return 2;
    }

    float fofSquare(float z)
    {
        return z*z;
    }

    IEnumerator moveTheObject()
    {


        yield return null;
    }
}

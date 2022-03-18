using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CrossProductCubeRoll))]
public class CrossProductInspector : Editor
{
    CrossProductCubeRoll cube;
    bool sceneRepaintAll;

    public Vector3 velocity;
    Vector3 sourceLocation;
    Vector3 destinationLocation;

    private void OnEnable()
    {
        cube = (CrossProductCubeRoll)target;

    }
    Vector3 direction;
    int i = 0;
    float speed;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Reset"))
        {
            cube.transform.position = new Vector3(0, 0f, 0);
            direction = Vector3.zero;
            cube.transform.rotation = Quaternion.identity;
            speed = 0;
        }
        sceneRepaintAll = EditorGUILayout.Toggle("SceneView Repaint ", sceneRepaintAll);


        if (GUILayout.Button("Run Physics"))
        {
            StepPhysics();
        }
        if (GUILayout.Button("Fire"))
        {
            cube.transform.rotation = Quaternion.identity;
            i = 0;
            sourceLocation = cube.path[i].position;
            destinationLocation = cube.path[i+1].position;
            direction =  destinationLocation - sourceLocation;
            cube.transform.position = sourceLocation;
            speed = 20;

        }

    }

    private void StepPhysics()
    {
        //https://answers.unity.com/questions/158766/physics-in-scene-editor-mode.html?childToView=1672642#answer-1672642
        Physics.autoSimulation = false;
        Physics.Simulate(Time.fixedDeltaTime);
        Physics.autoSimulation = true;
    }


    private void OnSceneGUI()
    {
        if (sceneRepaintAll)
        {
            StepPhysics();
            SceneView.RepaintAll();
        }

        if(Vector3.Distance(destinationLocation,cube.transform.position) > 0.1)
        {
            cube.transform.position += direction * 0.1f * Time.deltaTime;
            cube.transform.RotateAround(cube.transform.position, Vector3.Cross(direction,Vector3.up), speed * Time.deltaTime);

        }
        else
        {
            
            if (i < cube.path.Length - 1)
            {
                i = i + 1;
                sourceLocation = cube.path[i].position;
                if (i == cube.path.Length - 1) { 
                i = -1;
                    destinationLocation = cube.path[0].position;

                }
                else
                {
                    destinationLocation = cube.path[i + 1].position;

                }
                direction = destinationLocation - sourceLocation;
                cube.transform.position = sourceLocation;
                cube.transform.rotation = Quaternion.identity;

            }
            else {
                //Debug.Log("not moving");
            }

        }



    }
}

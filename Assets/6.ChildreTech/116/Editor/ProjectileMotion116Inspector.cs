using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProjectileMotion116))]
public class ProjectileMotion116Inspector : Editor
{


    ProjectileMotion116 cube;
    bool sceneRepaintAll;


    public Vector3 velocity;
    public Vector3 gravity;

    float slideYVelocity;
    float slideZVelocity;

    float slideGravity;




    bool showPlayerDetails;



    private void OnEnable()
    {
        cube = (ProjectileMotion116)target;
        gravity = new Vector3(0, 0, 0);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Reset"))
        {
            cube.transform.position = new Vector3(0, 0f,0);
            velocity = Vector3.zero;
            gravity = Vector3.zero;
            //cube.rb.velocity = Vector3.zero;
            //cube.rb.position = Vector3.zero;
            //cube.rb.centerOfMass = Vector3.zero;
        }
        slideYVelocity = EditorGUILayout.Slider("velocity Y ", slideYVelocity, 0, 50);
        slideZVelocity = EditorGUILayout.Slider("velocity Z", slideZVelocity, 0, 50);
        slideGravity = EditorGUILayout.Slider("gravity  ", slideGravity, -5, 5);
        sceneRepaintAll = EditorGUILayout.Toggle("SceneView Repaint ", sceneRepaintAll);
        showPlayerDetails = EditorGUILayout.Toggle("Show Player Details ", showPlayerDetails);
        EditorGUILayout.TextField("Velocity ", velocity.ToString());
        //showVelocity = EditorGUILayout.Foldout(showVelocity, "Velocity direction");
        if (GUILayout.Button("Run Physics"))
        {
            StepPhysics();
        }
        if (GUILayout.Button("Fire"))
        {
            cube.isgrounded = false;
            velocity = new Vector3(0, slideYVelocity, slideZVelocity);
            gravity= new Vector3(0, slideGravity, 0);
        }



    }

    private void StepPhysics()
    {
        //https://answers.unity.com/questions/158766/physics-in-scene-editor-mode.html?childToView=1672642#answer-1672642
        Physics.autoSimulation = false;
        Physics.Simulate(Time.fixedDeltaTime);
        Physics.autoSimulation = true;
    }
    bool isLeftShiftUp;
    bool isCubeStop;

    private void OnSceneGUI()
    {
        if (sceneRepaintAll)
        {
            StepPhysics();
            SceneView.RepaintAll();
        }
        Handles.color = Color.blue;
        Handles.DrawLine(Vector3.zero, cube.transform.position);


        if (showPlayerDetails)
        {
            //Handles.Label(cube.transform.position + (2 * new Vector3(1, 0, 1)), "Distance: " + cube.transform.position.magnitude);
            Handles.Label(cube.transform.position + (4 * Vector3.up) + (Vector3.right) * 2, "Velocity: " + velocity);
        }
        

        cube.transform.localPosition += velocity * Time.deltaTime;
        if (cube.isgrounded)
        {
            velocity = Vector3.zero;
        }
        if (!cube.isgrounded)
        {
            if(cube.transform.localPosition.y > 0)
            {
                Debug.Log("are we here");
                velocity += gravity;

            }
            else
            {
                cube.transform.localPosition = new Vector3(cube.transform.position.x,0, cube.transform.position.z);
                velocity = Vector3.zero;
            }

        }


    }





}

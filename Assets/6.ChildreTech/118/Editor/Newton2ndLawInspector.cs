using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Newton2ndLaw))]
public class Newton2ndLawInspector : Editor
{
    Newton2ndLaw cube;
    bool sceneRepaintAll;


    public Vector3 velocity;
    public Vector3 gravity;
    public Vector3 impulseForce;
    public Vector3 windForce;
    Vector3 sumOfForce;
    Vector3 accelaration;

    float slideYVelocity;
    float slideZVelocity;
    float slideWindForce;
    float slideImpusleForce;

    float slideGravity;

    bool showPlayerDetails;
    float massOfCube;


    private void OnEnable()
    {
        cube = (Newton2ndLaw)target;
        gravity = new Vector3(0, 0, 0);
        massOfCube = 1;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Reset"))
        {
            cube.transform.position = new Vector3(0, 0f, 0);
            velocity = Vector3.zero;
            gravity = Vector3.zero;
            //cube.rb.velocity = Vector3.zero;
            //cube.rb.position = Vector3.zero;
            //cube.rb.centerOfMass = Vector3.zero;
        }
        //slideYVelocity = EditorGUILayout.Slider("velocity Y ", slideYVelocity, 0, 50);
        //slideZVelocity = EditorGUILayout.Slider("velocity Z", slideZVelocity, 0, 50);
        slideGravity = EditorGUILayout.Slider("gravity  ", slideGravity, -5, 0);
        slideWindForce = EditorGUILayout.Slider("Wind Force Z ", slideWindForce, -5, 5);
        slideImpusleForce = EditorGUILayout.Slider("Impusle Force Y ", slideImpusleForce, 0, 15);
        massOfCube = EditorGUILayout.Slider("Mass  ", massOfCube, 0, 5);

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
            velocity = Vector3.zero;
            cube.isgrounded = false;
            windForce = new Vector3(0, 0, slideWindForce);
            impulseForce = new Vector3(0, slideImpusleForce, 0);
            gravity = new Vector3(0, slideGravity, 0);

            sumOfForce = gravity + windForce + impulseForce;

            accelaration = sumOfForce / massOfCube;
            velocity += accelaration;
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
        sumOfForce = gravity + windForce + impulseForce;

        //accelaration = sumOfForce / massOfCube;
        //velocity += accelaration;


        cube.transform.localPosition += velocity * Time.deltaTime;
        if (cube.isgrounded)
        {
            velocity = Vector3.zero;
        }
        if (!cube.isgrounded)
        {
            if (cube.transform.localPosition.y > 0)
            {
                accelaration = sumOfForce / massOfCube;
                velocity += accelaration;

            }
            else
            {
                cube.transform.localPosition = new Vector3(cube.transform.position.x, 0, cube.transform.position.z);
                velocity = Vector3.zero;
            }

        }



        impulseForce.Set(0, 0, 0);
        accelaration.Set(0, 0, 0);


    }


}
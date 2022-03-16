using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VelocityAccelaration114))]
public class VelocityAccelaration114Inspector : Editor
{
    VelocityAccelaration114 cube;
    Vector3 direction;
    bool showVelocity;
    bool sceneRepaintAll;
    float slideVelocity;
    float velocity = 0;
    bool[] wasdBool = new bool[4];

    public float slideJumpForce = 0;
    public float jumpForce = 0;
    public float leftForce = 0;
    public float forwardForce = 0;


    bool showPlayerDetails;
    public float YRotation;


    private void OnEnable()
    {
        cube = (VelocityAccelaration114)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Reset"))
        {
            cube.transform.position = Vector3.zero;
            direction = Vector3.zero;
            jumpForce = 0;
            cube.rb.velocity = Vector3.zero;
            cube.rb.centerOfMass = Vector3.zero;
            cube.rb.position = Vector3.zero;
            cube.transform.rotation = Quaternion.identity;
            cube.rb.rotation = Quaternion.identity;
            YRotation = 0;
        }
        slideVelocity = EditorGUILayout.Slider("velocity ", slideVelocity, 0, 5);
        slideJumpForce = EditorGUILayout.Slider("jumpForce  ", slideJumpForce, 0, 20);
        sceneRepaintAll = EditorGUILayout.Toggle("SceneView Repaint ", sceneRepaintAll);
        showPlayerDetails = EditorGUILayout.Toggle("Show Player Details ", showPlayerDetails);

        //showVelocity = EditorGUILayout.Foldout(showVelocity, "Velocity direction");
        if (GUILayout.Button("Run Physics"))
        {
            StepPhysics();
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
        Handles.color = Color.blue;
        Handles.DrawLine(Vector3.zero, cube.transform.position);

        Event e = Event.current;
        switch (e.type)
        {
            case EventType.KeyDown:
                {
                    if (Event.current.keyCode == (KeyCode.W))
                    {
                        direction = cube.transform.forward;
                        velocity = slideVelocity;
                        wasdBool[0] = true;
                    }
                    else if (Event.current.keyCode == (KeyCode.A))
                    {
                        direction = -cube.transform.right;
                        velocity = slideVelocity/2;
                        wasdBool[1] = true;
                        YRotation += -10;


                    }
                    else if (Event.current.keyCode == (KeyCode.S))
                    {
                        direction = -cube.transform.forward;
                        velocity = slideVelocity;
                        wasdBool[2] = true;

                    }
                    else if (Event.current.keyCode == (KeyCode.D))
                    {
                        direction = cube.transform.right;
                        velocity = slideVelocity/2;
                        wasdBool[3] = true;
                        YRotation += 10;
                    }
                    else if (Event.current.keyCode == (KeyCode.Space))
                    {
                        jumpForce = slideJumpForce;
                        if (wasdBool[0]) forwardForce = 2;
                        if (wasdBool[1]) leftForce = -2;
                        if (wasdBool[2]) forwardForce = -2;
                        if (wasdBool[3]) leftForce = 2;


                    }
                    else
                    {
                    }

                    break;
                }

            case EventType.KeyUp:
                {
                    if (Event.current.keyCode == (KeyCode.W))
                    {
                        wasdBool[0] = false;
                        if (countWASDBool() == 0)
                        {
                            velocity = 0;
                        }
                    }
                    else if (Event.current.keyCode == (KeyCode.A))
                    {
                        wasdBool[1] = false;
                        if (countWASDBool() == 0)
                        {
                            velocity = 0;
                        }
                    }
                    else if (Event.current.keyCode == (KeyCode.S))
                    {
                        wasdBool[2] = false;
                        if (countWASDBool() == 0)
                        {
                            velocity = 0;
                        }
                    }
                    else if (Event.current.keyCode == (KeyCode.D))
                    {
                        wasdBool[3] = false;
                        if (countWASDBool() == 0)
                        {
                            velocity = 0;
                        }
                    }
                    break;
                }
        }

        //cube.rb.AddForce(new Vector3(leftForce, jumpForce, forwardForce), ForceMode.Impulse);
        //cube.transform.rotation *= Quaternion.Euler(new Vector3(0, YRotation, 0)); 
        cube.transform.rotation = Quaternion.Lerp(cube.transform.rotation, Quaternion.Euler(new Vector3(0, YRotation, 0)), Time.deltaTime); 
        //YRotation = 0;
        //leftForce = 0;
        //forwardForce = 0;
        //jumpForce = 0;

        if (showPlayerDetails)
        {
            Handles.Label(cube.transform.position + (2 * new Vector3(1, 0, 1)), "Distance: " + cube.transform.position.magnitude);
            Handles.Label(cube.transform.position + (2 * Vector3.up) + (Vector3.right), "Velocity: " + velocity);
        }

        cube.transform.localPosition += direction * velocity * Time.deltaTime;

   
    }

    int countWASDBool()
    {
        int count = 0;
        for (int i = 0; i < wasdBool.Length; i++)
        {
            if (wasdBool[i] == true)
            {
                count++;
            }
        }
        return count;
    }

}

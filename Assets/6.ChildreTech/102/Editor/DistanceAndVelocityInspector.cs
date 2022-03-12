using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DistanceAndVelocity))]
public class DistanceAndVelocityInspector : Editor
{
    DistanceAndVelocity cube;
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

    Enemy102 enemyComponentScript;
    Vector3 enemyMoveDirec;
    Vector3 enemyToPlayerDistance;

    public GameObject Enemy;
    private void OnEnable()
    {
        cube = (DistanceAndVelocity)target;
        Enemy = GameObject.Find("Enemy");
        enemyComponentScript = Enemy.GetComponent<Enemy102>();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Reset"))
        {
            cube.transform.position = Vector3.zero;
            direction = Vector3.zero;
            jumpForce = 0;

        }
        slideVelocity = EditorGUILayout.Slider("velocity ", slideVelocity, 0, 5);
        slideJumpForce = EditorGUILayout.Slider("jumpForce  ", slideJumpForce, 0, 20);
        sceneRepaintAll = EditorGUILayout.Toggle("SceneView Repaint ", sceneRepaintAll);
        Enemy = (GameObject) EditorGUILayout.ObjectField("Enemy ", Enemy,  typeof(GameObject),true);

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
                        direction = Vector3.forward;
                        velocity = slideVelocity;
                        wasdBool[0] = true;
                    }
                    else if (Event.current.keyCode == (KeyCode.A))
                    {
                        direction = Vector3.left;
                        velocity = slideVelocity;
                        wasdBool[1] = true;

                    }
                    else if (Event.current.keyCode == (KeyCode.S))
                    {
                        direction = -Vector3.forward;
                        velocity = slideVelocity;
                        wasdBool[2] = true;

                    }
                    else if (Event.current.keyCode == (KeyCode.D))
                    {
                        direction = Vector3.right;
                        velocity = slideVelocity;
                        wasdBool[3] = true;
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

        cube.rb.AddForce(new Vector3(leftForce, jumpForce, forwardForce), ForceMode.Impulse);
        leftForce = 0;
        forwardForce = 0;
        jumpForce = 0;
        Handles.Label(cube.transform.position + (2 * Vector3.one), "Velocity: " + velocity);
        Handles.Label(Enemy.transform.position + (2 * Vector3.up), "Enemy ");
        Handles.Label(cube.transform.position + (2 * new Vector3(1, 0, 1)), "Disatance: " + cube.transform.position.magnitude);
        
        cube.transform.position += direction * velocity * Time.deltaTime;

        enemyMoveDirec = Vector3.zero;
        enemyToPlayerDistance = cube.transform.position - Enemy.transform.position;
        if (enemyToPlayerDistance.magnitude < enemyComponentScript.detectionRadius)
        {
            enemyMoveDirec = (cube.transform.position - Enemy.transform.position).normalized;
            Debug.Log(enemyMoveDirec.magnitude);

        }
        Enemy.transform.position += enemyMoveDirec * Time.deltaTime;
        enemyMoveDirec = Vector3.zero;
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

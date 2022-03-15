using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DistanceAndVelocity112))]
[CanEditMultipleObjects]
public class DistanceAndVelocityInspector112 : Editor
{
    DistanceAndVelocity112 cube;
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

    Enemy112 enemyComponentScript;
    Vector3 enemyMoveDirec;
    Vector3 enemyToPlayerDistance;

    public GameObject Enemy;

    bool showPlayerDetails;
    bool showEnemyDetails;

    private void OnEnable()
    {
        cube = (DistanceAndVelocity112)target;
        Enemy = GameObject.Find("Enemy");
        enemyComponentScript = Enemy.GetComponent<Enemy112>();
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
            enemyComponentScript.rb.velocity = Vector3.zero;
            enemyComponentScript.rb.position = Vector3.zero;
            Enemy.transform.position = Vector3.one;
        }
        slideVelocity = EditorGUILayout.Slider("velocity ", slideVelocity, 0, 5);
        slideJumpForce = EditorGUILayout.Slider("jumpForce  ", slideJumpForce, 0, 20);
        sceneRepaintAll = EditorGUILayout.Toggle("SceneView Repaint ", sceneRepaintAll);
        showPlayerDetails = EditorGUILayout.Toggle("Show Player Details ", showPlayerDetails);

        Enemy = (GameObject)EditorGUILayout.ObjectField("Enemy ", Enemy, typeof(GameObject), true);

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
        //Handles.Label(Enemy.transform.position + (2 * Vector3.up), "Enemy ");

        if(showPlayerDetails)
        {
            Handles.Label(cube.transform.position + (2 * new Vector3(1, 0, 1)), "Distance: " + cube.transform.position.magnitude);
            Handles.Label(cube.transform.position + (2 * Vector3.up) + (Vector3.right), "Velocity: " + velocity);
        }

        cube.transform.position += direction * velocity * Time.deltaTime;

        enemyMoveDirec = Vector3.zero;
        enemyToPlayerDistance = cube.transform.position - Enemy.transform.position;
        float rValue = Vector3.Dot(enemyToPlayerDistance.normalized,Enemy.transform.forward);
        float rAngle = Vector3.Angle(enemyToPlayerDistance.normalized, Enemy.transform.forward);

        //if (rValue == 1)
        //{
        //    Handles.Label(Enemy.transform.position + (2 * Vector3.up) + (Vector3.right), "Player in front " + rAngle);
        //}
        //else if (rValue == -1)
        //{
        //    Handles.Label(Enemy.transform.position + (2 * Vector3.up) + (Vector3.right), "Player in behind " + rAngle);
        //}
        //else
        //{
        //    Handles.Label(Enemy.transform.position + (2 * Vector3.up) + (Vector3.right), "value : " + rValue + " Angle : " + rAngle);
        //}




        if (enemyToPlayerDistance.magnitude < enemyComponentScript.detectionRadius)
        {
            enemyMoveDirec = ((cube.transform.position - Enemy.transform.position) - (2 * Vector3.forward)).normalized;
            //Debug.Log(Enemy.transform.position + enemyMoveDirec);
            Handles.color = Color.yellow;
            Handles.DrawLine(Enemy.transform.position, (Enemy.transform.position + enemyToPlayerDistance)); // enemyToPlayerDistance is un normalized vector direction from enemy to player

            if (rAngle < 90)
            {
                Handles.Label(Enemy.transform.position + (2 * Vector3.up) + (Vector3.right), "Player in front " + rAngle);
            }
            else
            {
                Handles.Label(Enemy.transform.position + (2 * Vector3.up) + (Vector3.right), "Player in behind " + rAngle);
            }


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

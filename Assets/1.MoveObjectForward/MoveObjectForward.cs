using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectForward : MonoBehaviour
{
    public float speed = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * speed;
        if (transform.position == new Vector3(0, 0, -1))
        {
            transform.position = transform.up;
            Debug.Log("is i got up");
        }
    }
}

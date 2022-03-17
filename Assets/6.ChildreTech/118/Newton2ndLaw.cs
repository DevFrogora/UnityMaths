using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Newton2ndLaw : MonoBehaviour
{
    //public Rigidbody rb;
    public bool isgrounded;

    private void OnEnable()
    {
        //rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision theCollision)
    {
        Debug.Log("We");
        if (theCollision.gameObject.name == "Plane")
        {
            isgrounded = true;
        }
    }

    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Plane")
        {
            isgrounded = false;
        }
    }
}

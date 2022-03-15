using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAndVelocity112 : MonoBehaviour
{
    public Rigidbody rb;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

}

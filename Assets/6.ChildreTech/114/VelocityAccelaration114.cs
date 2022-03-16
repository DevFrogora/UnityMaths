using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityAccelaration114 : MonoBehaviour
{
    public Rigidbody rb;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }
}

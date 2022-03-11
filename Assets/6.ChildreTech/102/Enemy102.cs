using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy102 : MonoBehaviour
{
    public float detectionAngle = 90.0f;
    public float detectionRadius = 4;
    private void OnDrawGizmos()
    {
        Handles.color = new Color(0.8f,0,0,0.2f);

        Vector3 rotatedForward = Quaternion.Euler(
            0,
            -detectionAngle * 0.5f,
            0) * transform.forward;
        Handles.DrawSolidArc(
            transform.position - Vector3.up,
            Vector3.up,
            rotatedForward,
            detectionAngle,
            detectionRadius
            );
    }
}

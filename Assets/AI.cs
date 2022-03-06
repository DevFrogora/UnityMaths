using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float areaRadius;
    public float speed;
    public Vector3[] nodePoints;
    public Quaternion[] nodePointsRoatation;
    public bool showNodeHandles = false;
    public bool showNodeHandlesRotation = false;
    public bool showNodeLines = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, areaRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * speed);
        Gizmos.color = Color.yellow;
        for (int i = 0; i < nodePoints.Length; i++)
        {
            Gizmos.DrawSphere(nodePoints[i], 0.5f);
        }
        Gizmos.color = Color.white;

        Gizmos.DrawIcon(transform.position, "PlayerGizmos.png",true); 
    }

    private void OnDrawGizmosSelected()
    {

    }
}

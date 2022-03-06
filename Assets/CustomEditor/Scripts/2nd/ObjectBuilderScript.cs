using UnityEngine;
using System.Collections;

public class ObjectBuilderScript : MonoBehaviour
{
    public GameObject obj;
    public Vector3 spawnPoint;
    public int totalChild = 0;


    public void BuildObject()
    {
        Instantiate(obj, spawnPoint, Quaternion.identity).gameObject.transform.SetParent(gameObject.transform);
        totalChild++;
    }
}

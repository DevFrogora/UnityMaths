using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
// To delink the editor script comment the custom Editor header
[CustomEditor(typeof(AngleOfObjectFromPoint))]
public class AngleOfObjectFromPointInspector : Editor
{

     AngleOfObjectFromPoint cube;
    void OnEnable()
    {
        cube = (AngleOfObjectFromPoint)(target);

    }
    Vector3 directionA;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

    }

    private void OnSceneGUI()
    {

    }
}

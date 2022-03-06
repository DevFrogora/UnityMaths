using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBehaviour : MonoBehaviour
{
    [ContextMenuItem("Randomize Name", "Randomize")]
    public string Name;

    private void Randomize()
    {
        Name = "Some Random Name";
    }

    [ContextMenu("Reset Name")]
    private void ResetName()
    {
        Name = string.Empty;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Player))]
[CanEditMultipleObjects]
public class PlayerInspector2 : Editor
{
    Player player;
    SerializedProperty playerName;
    SerializedProperty playerHealth;

    private void OnEnable()
    {
        player = (Player)target;
        playerName = serializedObject.FindProperty("playerName");
        playerHealth = serializedObject.FindProperty("health");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(playerName);

        if (playerHealth.floatValue < 33)
        {
            GUI.color = Color.red;
        }

        if (playerHealth.floatValue > 33)
        {
            GUI.color = new Color(1.0f, 0.64f, 0.0f);
        }
        if (playerHealth.floatValue > 66)
        {
            GUI.color = Color.green;
        }
        EditorGUILayout.PropertyField(playerHealth);
        GUI.color = Color.white;

        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();

    }

}

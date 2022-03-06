using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// To delink the editor script comment the custom Editor header
// [CustomEditor(typeof(Player))]
public class PlayerInspector : Editor
{
    Player player;
    bool showWeapons;
    bool showShoes;
    EditorWindow window;

    private void OnEnable()
    {
        player =(Player) target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("This is our Player Inspector");
        EditorGUILayout.LabelField("Player id - "+player.id);
        player.playerName = EditorGUILayout.TextArea(player.playerName);
        

        EditorGUILayout.LabelField("Back Story");
        player.backStory = EditorGUILayout.TextArea(player.backStory,GUILayout.MinHeight(70));
        
        if(player.health < 33 )
        {
            GUI.color = Color.red;
        }

        if (player.health > 33)
        {
            GUI.color = new Color(1.0f, 0.64f, 0.0f);
        }
        if (player.health > 66)
        {
            GUI.color = Color.green;
        }
        Rect progressRect = GUILayoutUtility.GetRect(50, 10);
        EditorGUI.ProgressBar(progressRect, player.health / 100.0f, "Health");
        GUI.color = Color.white;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        player.damage = EditorGUILayout.Slider("Damage", player.damage, 10, 20);

        if(player.damage < 12)
        {
            EditorGUILayout.HelpBox("Damage is too low !", MessageType.Warning);
        }
        if (player.damage > 18)
        {
            EditorGUILayout.HelpBox("Damge is too damn high !", MessageType.Warning);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        showWeapons = EditorGUILayout.Foldout(showWeapons, "Weapons");

        if(showWeapons)
        {
            EditorGUI.indentLevel += 2;
            player.weapon1Damage = EditorGUILayout.FloatField("Weapon 1 Damage", player.weapon1Damage);
            player.weapon2Damage = EditorGUILayout.FloatField("Weapon 2 Damage", player.weapon2Damage);
            EditorGUI.indentLevel -= 2;
        }


        showShoes = EditorGUILayout.Foldout(showShoes, "Player Shoes");

        if(showShoes)
        {
            /*
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Name",GUILayout.MaxWidth(50));
            player.shoeName = EditorGUILayout.TextField(player.shoeName);
            EditorGUILayout.LabelField("Size", GUILayout.MaxWidth(50));
            player.shoeSize = EditorGUILayout.IntField(player.shoeSize);
            EditorGUILayout.LabelField("Type", GUILayout.MaxWidth(50));
            player.shoeType = EditorGUILayout.TextField(player.shoeType);
            EditorGUILayout.EndHorizontal();
            */
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();


        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Health Revive"))
        {
            player.health = 100;
        }
        if (GUILayout.RepeatButton("Fire on player"))
        {
            player.health -= player.damage / 20;
        }
        if (GUILayout.Button("Random Player Name"))
        {
            player.RandomNameForPlayer();
        }

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Windows"))
        {
            window = MyFirstEditorWindow.RefToWindow();
        }
        if (GUILayout.Button("Close Windows"))
        {
            window.Close();
        }

        EditorGUILayout.EndVertical();

        

        if(player.health > 100)
        {
            player.health = 0;
        }
    }
}

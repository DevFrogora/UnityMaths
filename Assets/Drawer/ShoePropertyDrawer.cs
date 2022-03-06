using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Shoe))]
public class ShoePropertyDrawer : PropertyDrawer
{
    Rect top, middleRight, middleLeft, bottom;
    SerializedProperty shoeName, shoeType, size, description;
    string descriptionString ;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //GUI.Label(position, "Shoe Custom Drawer");
        top = new Rect(position.x, position.y, position.width, position.height / 4f);
        middleRight = new Rect(position.x, 
            position.y + (position.height /4.0f) * 1, 
            position.width /2.0f,
            position.height / 4f);

        middleLeft = new Rect(position.x + (position.width)/2f,
            position.y + (position.height / 4.0f) * 1,
            position.width / 2.0f,
            position.height / 4f);
        bottom = new Rect(position.x,
            position.y + (position.height / 4.0f) * 2,
            position.width ,
            position.height / 2f);

        shoeName = property.FindPropertyRelative("name");
        shoeType = property.FindPropertyRelative("shoeType");
        size = property.FindPropertyRelative("size");
        description = property.FindPropertyRelative("description");

        EditorGUI.PropertyField(top,shoeName , new GUIContent("Shoe Name"));
        EditorGUI.PropertyField(middleRight, shoeType);
        EditorGUI.PropertyField(middleLeft, size);

        descriptionString = description.stringValue;
        descriptionString = EditorGUI.TextArea(bottom, descriptionString);
        description.stringValue = descriptionString;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * 4f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyWithColorAttribute))]
public class ReadOnlyWithColorAttributeDrawer : PropertyDrawer
{
    string value;
    Color inputColor;
    ReadOnlyWithColorAttribute _attribute = new ReadOnlyWithColorAttribute();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _attribute = (ReadOnlyWithColorAttribute)attribute;
        inputColor = new Color(_attribute.r, _attribute.g, _attribute.b);

        if (property.propertyType == SerializedPropertyType.Integer)
            value = property.intValue.ToString();
        if (property.propertyType == SerializedPropertyType.Float)
            value = property.floatValue.ToString();
        if (property.propertyType == SerializedPropertyType.String)
            value = property.stringValue.ToString();

        GUI.color = inputColor;


        //https://answers.unity.com/questions/1259020/how-do-i-change-the-text-color-on-guilabel.html
        //https://answers.unity.com/questions/17683/custom-font-in-guilabel-but-cant-change-its-color.html

        var style = new GUIStyle();
        //style.fontSize = 70;
        style.normal.textColor = inputColor;

        GUI.Label(position, property.displayName + (_attribute.r, _attribute.g, _attribute.b) + " : " + value, style);
        style.normal.textColor = Color.black;


    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}

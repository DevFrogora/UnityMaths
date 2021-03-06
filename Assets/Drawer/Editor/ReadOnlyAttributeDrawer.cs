using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyAttributeDrawer : PropertyDrawer
{
    string value;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(property.propertyType == SerializedPropertyType.Integer)
            value = property.intValue.ToString();
        if (property.propertyType == SerializedPropertyType.Float)
            value = property.floatValue.ToString();
        if (property.propertyType == SerializedPropertyType.String)
            value = property.stringValue.ToString();


        GUI.Label(position, property.displayName+" : "+value);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}

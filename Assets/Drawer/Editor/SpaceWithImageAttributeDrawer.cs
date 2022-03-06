using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SpaceWithImageAttribute))]
public class SpaceWithImageAttributeDrawer : DecoratorDrawer
{

    Texture2D image;
    SpaceWithImageAttribute _attribute = new SpaceWithImageAttribute();
    public override void OnGUI(Rect position)
    {
       if(image == null)
        {
            image = Resources.Load<Texture2D>("CharacterImage");
        }
        _attribute = (SpaceWithImageAttribute)attribute;
        GUI.DrawTexture(position, image);
    }

    public override float GetHeight()
    {
        return base.GetHeight() + _attribute.height;
    }
}

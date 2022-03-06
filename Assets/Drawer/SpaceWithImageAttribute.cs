using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWithImageAttribute : PropertyAttribute
{
    public int height;
    public SpaceWithImageAttribute()
    {
        height = 30;
    }

    public SpaceWithImageAttribute(int _height)
    {
        height = _height;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MyVector2
{
    public float x;
    public float y;
    public MyVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public static implicit operator Vector2(MyVector2 v)
    {
        return new Vector2(v.x, v.y);
    }
    public static implicit operator MyVector2(Vector2 v)
    {
        return new MyVector2(v.x, v.y);
    }
    public static implicit operator Vector3(MyVector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    public override string ToString()
    {
        return "(" + x.ToString() + ", " + y.ToString() + ")";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MyVector3
{
    public float x;
    public float y;
    public float z;
    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public static implicit operator Vector3(MyVector3 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }
    public static implicit operator MyVector3(Vector3 v)
    {
        return new MyVector3(v.x, v.y, v.z);
    }
}

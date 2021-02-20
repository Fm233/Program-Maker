using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct MoveInf
{
    public SelectInf si;
    public Vector3 pos;

    public MoveInf(SelectInf si, Vector3 pos)
    {
        this.si = si;
        this.pos = pos;
    }
}

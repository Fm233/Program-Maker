using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct NodeInf
{
    public SelectInf si;
    public MyVector2 pos;

    public NodeInf(SelectInf si, MyVector2 pos)
    {
        this.si = si;
        this.pos = pos;
    }
}

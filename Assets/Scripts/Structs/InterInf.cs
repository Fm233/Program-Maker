using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct InterInf
{
    public SelectInf si;
    public MyVector2 pos;

    public InterInf(SelectInf si, MyVector2 pos)
    {
        this.si = si;
        this.pos = pos;
    }
}

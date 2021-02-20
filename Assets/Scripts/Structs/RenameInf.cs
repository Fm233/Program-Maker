using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct RenameInf
{
    public SelectInf si;
    public string name;

    public RenameInf(SelectInf si, string name)
    {
        this.si = si;
        this.name = name;
    }
}

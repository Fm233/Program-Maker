using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct ConnInf
{
    public SelectInf self;
    public SelectInf start;
    public SelectInf end;

    public ConnInf(SelectInf self, SelectInf start, SelectInf end)
    {
        this.self = self;
        this.start = start;
        this.end = end;
    }
}

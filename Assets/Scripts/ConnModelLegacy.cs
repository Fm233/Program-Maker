using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ConnModelExport
{
    public int a;
    public int b;
    public string text;
    public bool avaliable;

    public ConnModelExport(int a, int b, string text, bool avaliable)
    {
        this.a = a;
        this.b = b;
        this.text = text;
        this.avaliable = avaliable;
    }
}

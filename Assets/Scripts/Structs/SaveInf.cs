using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SaveInf
{
    public string name;
    public Vector3 canvasPos;
    public int buildIndex;

    public SaveInf(string name, Vector3 canvasPos, int buildIndex)
    {
        this.name = name;
        this.canvasPos = canvasPos;
        this.buildIndex = buildIndex;
    }
}

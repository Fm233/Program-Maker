using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LoadInf
{
    public Vector3 canvasPos;
    public int buildCount;

    public LoadInf(Vector3 canvasPos, int buildCount)
    {
        this.canvasPos = canvasPos;
        this.buildCount = buildCount;
    }
}

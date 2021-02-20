using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct TryConnInf
{
    public TryConnInfType type;
    public Vector2 pos;

    public TryConnInf(TryConnInfType type, Vector2 pos)
    {
        this.type = type;
        this.pos = pos;
    }
    public static TryConnInf end
    {
        get
        {
            return new TryConnInf(TryConnInfType.END, Vector2.zero);
        }
    }
}

public enum TryConnInfType
{
    START,
    POS,
    END
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InterModel : IElementModel, IModelMoveable
{
    public InterInf interInf;

    public InterModel(InterInf interInf)
    {
        this.interInf = interInf;
    }

    public SelectInf selection
    {
        get
        {
            return interInf.si;
        }
    }

    public SelectInf GetSelectInf()
    {
        return interInf.si;
    }

    public void Move(Vector2 pos)
    {
        interInf.pos = pos;
    }
}

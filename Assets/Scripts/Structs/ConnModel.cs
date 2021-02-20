using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConnModel : IElementModel, IModelRenameable
{
    public string content;
    public ConnInf connInf;

    public ConnModel(string content, ConnInf connInf)
    {
        this.content = content;
        this.connInf = connInf;
    }

    public SelectInf selection
    {
        get
        {
            return connInf.self;
        }
    }

    public SelectInf GetSelectInf()
    {
        return connInf.self;
    }

    public void Rename(string newName)
    {
        content = newName;
    }
}

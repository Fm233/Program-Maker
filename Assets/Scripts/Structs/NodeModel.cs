using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NodeModel : IElementModel, IModelRenameable, IModelMoveable
{
    public string name;
    public NodeInf nodeInf;

    public NodeModel(string name, NodeInf nodeInf)
    {
        this.name = name;
        this.nodeInf = nodeInf;
    }

    public SelectInf selection
    {
        get
        {
            return nodeInf.si;
        }
    }

    public SelectInf GetSelectInf()
    {
        return nodeInf.si;
    }

    public void Move(Vector2 pos)
    {
        nodeInf.pos = pos;
    }

    public void Rename(string newName)
    {
        name = newName;
    }
}

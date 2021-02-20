using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ProjectLegacy
{
    public NodeModelExport[] nodes;
    public ConnModelExport[] conns;
    public MyVector3 canvasPos;
    public int builds;
}

[Serializable]
public struct Project
{
    public NodeModel[] nodes;
    public ConnModel[] conns;
    public InterModel[] inters;
    public MyVector3 canvasPos;
    public int builds;

    public Project(NodeModel[] nodes, ConnModel[] conns, InterModel[] inters, MyVector3 canvasPos, int builds)
    {
        this.nodes = nodes;
        this.conns = conns;
        this.inters = inters;
        this.canvasPos = canvasPos;
        this.builds = builds;
    }
}

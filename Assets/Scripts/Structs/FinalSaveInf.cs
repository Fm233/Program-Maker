using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FinalSaveInf
{
    public List<IElementModel> models;
    public string name;
    public Vector3 canvasPos;
    public int buildIndex;

    public FinalSaveInf(List<IElementModel> models, string name, Vector3 canvasPos, int buildIndex)
    {
        this.models = models;
        this.name = name;
        this.canvasPos = canvasPos;
        this.buildIndex = buildIndex;
    }
}

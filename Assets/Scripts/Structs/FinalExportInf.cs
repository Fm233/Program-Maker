using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct FinalExportInf
{
    public List<IElementModel> models;
    public int buildCount;

    public FinalExportInf(List<IElementModel> models, int buildCount)
    {
        this.models = models;
        this.buildCount = buildCount;
    }
}

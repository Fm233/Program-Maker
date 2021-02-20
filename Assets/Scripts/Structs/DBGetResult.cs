using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct DBGetResult
{
    public List<IElementModel> elements;

    public DBGetResult(List<IElementModel> elements)
    {
        this.elements = elements;
    }

    public IElementModel GetOneElement()
    {
        if (elements.Count > 0)
        {
            return elements[0];
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct DBCreateRequest
{
    public IElementModel element;

    public DBCreateRequest(IElementModel element)
    {
        this.element = element;
    }
}

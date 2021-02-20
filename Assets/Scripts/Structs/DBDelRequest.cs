using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct DBDelRequest
{
    public Predicate<IElementModel> selector;

    public DBDelRequest(Predicate<IElementModel> selector)
    {
        this.selector = selector;
    }
}

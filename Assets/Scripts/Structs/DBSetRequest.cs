using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct DBSetRequest
{
    public Predicate<SelectInf> selector;
    public Action<IElementModel> setAction;

    public DBSetRequest(Predicate<SelectInf> selector, Action<IElementModel> setAction)
    {
        this.selector = selector;
        this.setAction = setAction;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public struct DBGetRequest
{
    public Predicate<SelectInf> selector;
    public Action<DBGetResult> returnAction;

    public DBGetRequest(Predicate<SelectInf> selector, Action<DBGetResult> returnAction)
    {
        this.selector = selector;
        this.returnAction = returnAction;
    }
}

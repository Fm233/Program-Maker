using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ProMove : MonoBehaviour, IInMoveToProMoveReceiver, IProMoveToDBSetSender, IDBGetter, IProMoveToOutMoveSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<MoveInf> GetPmiReceiver()
    {
        return ReceivePmi;
    }
    public void ReceivePmi(MoveInf pmi)
    {
        inDbsrAction(new DBSetRequest((SelectInf si) =>
        {
            return pmi.si == si;
        }, (IElementModel element) =>
        {
            if (element is IModelMoveable)
            {
                IModelMoveable moveable = element as IModelMoveable;
                moveable.Move(pmi.pos);
            }
        }));
        fmiAction(pmi);
    }
    Action<DBSetRequest> inDbsrAction;
    public void AddInDbsrReceiver(Action<DBSetRequest> action)
    {
        inDbsrAction += action;
    }
    Action<DBGetRequest> inDbgrAction;
    public void AddInDbgrReceiver(Action<DBGetRequest> action)
    {
        inDbgrAction += action;
    }
    Action<MoveInf> fmiAction;
    public void AddFmiReceiver(Action<MoveInf> action)
    {
        fmiAction += action;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ProRename : MonoBehaviour, IInRenameToProRenameReceiver, IProRenameToDBSetSender, IDBGetter, IProRenameToOutRenameSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<RenameInf> GetPriReceiver()
    {
        return ReceivePri;
    }
    public void ReceivePri(RenameInf pri)
    {
        inDbsrAction(new DBSetRequest((SelectInf si) =>
        {
            return pri.si == si;
        }, (IElementModel element) =>
        {
            if (element is IModelRenameable)
            {
                IModelRenameable renameable = element as IModelRenameable;
                renameable.Rename(pri.name);
            }
        }));
        friAction(pri);
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
    Action<RenameInf> friAction;
    public void AddFriReceiver(Action<RenameInf> action)
    {
        friAction += action;
    }
}

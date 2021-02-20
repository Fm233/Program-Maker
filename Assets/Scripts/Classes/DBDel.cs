using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class DBDel : MonoBehaviour, IDBDelToDBSender, IProDeleteNodeToDBDelReceiver
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    Action<DBDelRequest> outDbdrAction;
    public void AddOutDbdrReceiver(Action<DBDelRequest> action)
    {
        outDbdrAction += action;
    }
    public Action<DBDelRequest> GetInDbdrReceiver()
    {
        return ReceiveInDbdr;
    }
    public void ReceiveInDbdr(DBDelRequest inDbdr)
    {
        outDbdrAction(inDbdr);
    }
}

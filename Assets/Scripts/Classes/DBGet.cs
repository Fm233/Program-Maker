using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class DBGet : MonoBehaviour, IDBGetToDBSender, IDBGet
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    Action<DBGetRequest> outDbgrAction;
    public void AddOutDbgrReceiver(Action<DBGetRequest> action)
    {
        outDbgrAction += action;
    }
    public Action<DBGetRequest> GetInDbgrReceiver()
    {
        return ReceiveInDbgr;
    }
    public void ReceiveInDbgr(DBGetRequest inDbgr)
    {
        outDbgrAction(inDbgr);
    }
}

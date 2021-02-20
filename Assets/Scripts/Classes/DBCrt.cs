using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class DBCrt : MonoBehaviour, IDBCrtToDBSender, IDBCrt
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    Action<DBCreateRequest> outDbcrAction;
    public void AddOutDbcrReceiver(Action<DBCreateRequest> action)
    {
        outDbcrAction += action;
    }
    public Action<DBCreateRequest> GetInDbcrReceiver()
    {
        return ReceiveInDbcr;
    }
    public void ReceiveInDbcr(DBCreateRequest inDbcr)
    {
        outDbcrAction(inDbcr);
    }
}

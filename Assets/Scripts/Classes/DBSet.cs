using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class DBSet : MonoBehaviour, IDBSetToDBSender, IDBSet
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    Action<DBSetRequest> outDbsrAction;
    public void AddOutDbsrReceiver(Action<DBSetRequest> action)
    {
        outDbsrAction += action;
    }
    public Action<DBSetRequest> GetInDbsrReceiver()
    {
        return ReceiveInDbsr;
    }
    public void ReceiveInDbsr(DBSetRequest inDbsr)
    {
        outDbsrAction(inDbsr);
    }
}

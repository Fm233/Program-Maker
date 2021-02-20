using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class DBRes : MonoBehaviour, IDBToDBResReceiver
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<DBGetResult> GetInDbgrReceiver()
    {
        return ReceiveInDbgr;
    }
    public void ReceiveInDbgr(DBGetResult inDbgr)
    {
        // Fill receiver function here
    }
}

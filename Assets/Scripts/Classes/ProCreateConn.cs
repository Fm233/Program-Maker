using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ProCreateConn : MonoBehaviour, IExPanelToProCreateConnReceiver, IDBCrter, IDBGetter, IProCreateConnToOutCreateConnSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<ConnInf> GetIciReceiver()
    {
        return ReceiveIci;
    }
    public void ReceiveIci(ConnInf ici)
    {
        inDbcrAction(new DBCreateRequest(new ConnModel("", ici)));
        fciAction(ici);
    }
    Action<DBCreateRequest> inDbcrAction;
    public void AddInDbcrReceiver(Action<DBCreateRequest> action)
    {
        inDbcrAction += action;
    }
    Action<DBGetRequest> inDbgrAction;
    public void AddInDbgrReceiver(Action<DBGetRequest> action)
    {
        inDbgrAction += action;
    }
    Action<ConnInf> fciAction;
    public void AddFciReceiver(Action<ConnInf> action)
    {
        fciAction += action;
    }
}

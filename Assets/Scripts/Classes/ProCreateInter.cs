using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ProCreateInter : MonoBehaviour, IExPanelToProCreateInterReceiver, IProCreateInterToOutCreateInterSender, IDBGetter, IDBCrter
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<InterInf> GetIiiReceiver()
    {
        return ReceiveIii;
    }
    public void ReceiveIii(InterInf iii)
    {
        inDbcrAction(new DBCreateRequest(new InterModel(iii)));
        fiiAction(iii);
    }
    Action<InterInf> fiiAction;
    public void AddFiiReceiver(Action<InterInf> action)
    {
        fiiAction += action;
    }
    Action<DBGetRequest> inDbgrAction;
    public void AddInDbgrReceiver(Action<DBGetRequest> action)
    {
        inDbgrAction += action;
    }
    Action<DBCreateRequest> inDbcrAction;
    public void AddInDbcrReceiver(Action<DBCreateRequest> action)
    {
        inDbcrAction += action;
    }
}

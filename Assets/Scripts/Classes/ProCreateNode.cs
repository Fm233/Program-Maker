using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ProCreateNode : MonoBehaviour, IExPanelToProCreateNodeReceiver, IDBCrter, IDBGetter, IProCreateNodeToOutCreateNodeSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<NodeInf> GetIniReceiver()
    {
        return ReceiveIni;
    }
    public void ReceiveIni(NodeInf ini)
    {
        inDbcrAction(new DBCreateRequest(new NodeModel("", ini)));
        fniAction(ini);
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
    Action<NodeInf> fniAction;
    public void AddFniReceiver(Action<NodeInf> action)
    {
        fniAction += action;
    }
}

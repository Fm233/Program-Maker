using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ProExport : MonoBehaviour, IInExportToProExportReceiver, IDBGetter, IProExportToOutExportSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<ExportInf> GetPeiReceiver()
    {
        return ReceivePei;
    }
    public void ReceivePei(ExportInf pei)
    {
        inDbgrAction(new DBGetRequest((SelectInf inf) =>
        {
            return true;
        }, (DBGetResult result) =>
        {
            feiAction(new FinalExportInf(result.elements, pei.buildCount));
        }));
    }
    Action<DBGetRequest> inDbgrAction;
    public void AddInDbgrReceiver(Action<DBGetRequest> action)
    {
        inDbgrAction += action;
    }
    Action<FinalExportInf> feiAction;
    public void AddFeiReceiver(Action<FinalExportInf> action)
    {
        feiAction += action;
    }
}

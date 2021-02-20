using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ProTryConn : MonoBehaviour, IExPanelToProTryConnReceiver, IDBGetter
{
    Action<DBGetRequest> dbgrAction;
    public LineRenderer lineRenderer;
    Vector3 startPos;
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<TryConnInf> GetTciReceiver()
    {
        return ReceiveTci;
    }
    public void ReceiveTci(TryConnInf tci)
    {
        if (tci.type == TryConnInfType.START)
        {
            lineRenderer.enabled = true;
            startPos = tci.pos;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, startPos);
        }
        if (tci.type == TryConnInfType.POS)
        {
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, tci.pos);
        }
        if (tci.type == TryConnInfType.END)
        {
            lineRenderer.enabled = false;
        }
    }

    public void AddInDbgrReceiver(Action<DBGetRequest> action)
    {
        dbgrAction = action;
    }
}

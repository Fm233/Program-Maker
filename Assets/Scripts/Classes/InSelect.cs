using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class InSelect : MonoBehaviour, INodeToInSelectReceiver, IInSelectToExPanelSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<SelectInf> GetSiReceiver()
    {
        return ReceiveSi;
    }
    public void ReceiveSi(SelectInf nsi)
    {
        siAction(nsi);
    }
    Action<SelectInf> siAction;
    public void AddSiReceiver(Action<SelectInf> action)
    {
        siAction += action;
    }
}

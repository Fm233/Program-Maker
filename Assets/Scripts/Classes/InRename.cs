using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class InRename : MonoBehaviour, IInRenameToProRenameSender, INodeToInRenameReceiver
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    Action<RenameInf> priAction;
    public void AddPriReceiver(Action<RenameInf> action)
    {
        priAction += action;
    }
    public Action<RenameInf> GetRiReceiver()
    {
        return ReceiveNri;
    }
    public void ReceiveNri(RenameInf nri)
    {
        priAction(nri);
    }
}

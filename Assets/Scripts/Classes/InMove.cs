using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class InMove : MonoBehaviour, IMoveReceiver, IInMoveToProMoveSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<MoveInf> GetMiReceiver()
    {
        return ReceiveMi;
    }
    public void ReceiveMi(MoveInf dmi)
    {
        pmiAction(dmi);
    }
    Action<MoveInf> pmiAction;
    public void AddPmiReceiver(Action<MoveInf> action)
    {
        pmiAction += action;
    }
}

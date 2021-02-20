using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class OutMove : MonoBehaviour, IProMoveToOutMoveReceiver
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<MoveInf> GetFmiReceiver()
    {
        return ReceiveFmi;
    }
    public void ReceiveFmi(MoveInf fmi)
    {
        GameObject.Find(fmi.si.ToString()).transform.localPosition = fmi.pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class InRC : MonoBehaviour, IInRCToExPanelSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rcPosAction(Input.mousePosition);
        }
    }
    Action<Vector2> rcPosAction;
    public void AddRcPosReceiver(Action<Vector2> action)
    {
        rcPosAction += action;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;
public class InDC : MonoBehaviour, IInDCToExPanelSender
{
    Stopwatch dcTimer = new Stopwatch();
    bool justdced;
    void Start()
    {
        dcTimer.Start();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dcTimer.ElapsedMilliseconds < 200f && !justdced)
            {
                dcPosAction(Input.mousePosition);
                justdced = true;
            }
            else
            {
                dcTimer.Restart();
                justdced = false;
            }
        }
    }
    Action<Vector2> dcPosAction;
    public void AddDcPosReceiver(Action<Vector2> action)
    {
        dcPosAction += action;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class InMouse : MonoBehaviour, IInMouseToExPanelSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        mousePosAction(Input.mousePosition);
    }
    Action<Vector2> mousePosAction;
    public void AddMousePosReceiver(Action<Vector2> action)
    {
        mousePosAction += action;
    }
}

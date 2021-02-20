using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class InDelete : MonoBehaviour, IInDeleteToExPanelSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            deletePosAction(Input.mousePosition);
        }
    }
    Action<Vector2> deletePosAction;
    public void AddDeletePosReceiver(Action<Vector2> action)
    {
        deletePosAction += action;
    }
}

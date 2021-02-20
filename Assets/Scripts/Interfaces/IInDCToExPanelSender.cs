using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInDCToExPanelSender
{
    void AddDcPosReceiver(Action<Vector2> action);
}

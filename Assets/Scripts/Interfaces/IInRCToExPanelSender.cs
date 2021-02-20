using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInRCToExPanelSender
{
    void AddRcPosReceiver(Action<Vector2> action);
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInDeleteToExPanelSender
{
    void AddDeletePosReceiver(Action<Vector2> action);
}

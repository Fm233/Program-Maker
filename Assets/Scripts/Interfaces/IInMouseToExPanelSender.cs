using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInMouseToExPanelSender
{
    void AddMousePosReceiver(Action<Vector2> action);
}

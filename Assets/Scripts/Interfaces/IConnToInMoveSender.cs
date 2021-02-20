using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IConnToInMoveSender
{
    void AddDmiReceiver(Action<MoveInf> action);
}

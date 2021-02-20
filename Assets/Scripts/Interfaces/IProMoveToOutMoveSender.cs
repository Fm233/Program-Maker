using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProMoveToOutMoveSender
{
    void AddFmiReceiver(Action<MoveInf> action);
}

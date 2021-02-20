using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInMoveToProMoveSender
{
    void AddPmiReceiver(Action<MoveInf> action);
}

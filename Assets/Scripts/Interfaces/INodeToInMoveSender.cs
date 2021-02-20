using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IMoveSender
{
    void AddMiReceiver(Action<MoveInf> action);
}

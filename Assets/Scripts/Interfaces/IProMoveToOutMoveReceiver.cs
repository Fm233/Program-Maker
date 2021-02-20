using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProMoveToOutMoveReceiver
{
    Action<MoveInf> GetFmiReceiver();
}

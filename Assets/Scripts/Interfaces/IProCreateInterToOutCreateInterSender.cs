using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProCreateInterToOutCreateInterSender
{
    void AddFiiReceiver(Action<InterInf> action);
}

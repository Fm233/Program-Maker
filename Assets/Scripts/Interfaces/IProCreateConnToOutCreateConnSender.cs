using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProCreateConnToOutCreateConnSender
{
    void AddFciReceiver(Action<ConnInf> action);
}

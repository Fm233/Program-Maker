using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IExPanelToProTryConnSender
{
    void AddTciReceiver(Action<TryConnInf> action);
}

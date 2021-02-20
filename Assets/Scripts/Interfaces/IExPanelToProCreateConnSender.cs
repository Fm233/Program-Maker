using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IExPanelToProCreateConnSender
{
    void AddIciReceiver(Action<ConnInf> action);
}

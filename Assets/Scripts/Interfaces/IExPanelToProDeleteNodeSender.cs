using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IExPanelToProDeleteNodeSender
{
    void AddIdiReceiver(Action<DeleteInf> action);
}

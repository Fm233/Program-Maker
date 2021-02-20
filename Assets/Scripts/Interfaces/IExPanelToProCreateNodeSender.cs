using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IExPanelToProCreateNodeSender
{
    void AddIniReceiver(Action<NodeInf> action);
}

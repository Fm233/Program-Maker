using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProCreateNodeToOutCreateNodeSender
{
    void AddFniReceiver(Action<NodeInf> action);
}

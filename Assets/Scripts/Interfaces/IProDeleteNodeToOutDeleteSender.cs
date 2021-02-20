using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProDeleteNodeToOutDeleteSender
{
    void AddFdiReceiver(Action<DeleteInf> action);
}

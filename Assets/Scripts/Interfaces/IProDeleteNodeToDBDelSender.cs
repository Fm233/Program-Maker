using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProDeleteNodeToDBDelSender
{
    void AddInDbdrReceiver(Action<DBDelRequest> action);
}

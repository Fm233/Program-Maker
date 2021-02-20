using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProMoveToDBSetSender
{
    void AddInDbsrReceiver(Action<DBSetRequest> action);
}

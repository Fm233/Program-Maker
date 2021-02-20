using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IDBSetToDBSender
{
    void AddOutDbsrReceiver(Action<DBSetRequest> action);
}

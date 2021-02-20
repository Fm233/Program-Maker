using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IDBGetToDBSender
{
    void AddOutDbgrReceiver(Action<DBGetRequest> action);
}

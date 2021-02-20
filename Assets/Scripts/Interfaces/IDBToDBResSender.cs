using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IDBToDBResSender
{
    void AddInDbgrReceiver(Action<DBGetResult> action);
}

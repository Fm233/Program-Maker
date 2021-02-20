using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IDBGetter
{
    void AddInDbgrReceiver(Action<DBGetRequest> action);
}

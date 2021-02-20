using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IDBCrter
{
    void AddInDbcrReceiver(Action<DBCreateRequest> action);
}

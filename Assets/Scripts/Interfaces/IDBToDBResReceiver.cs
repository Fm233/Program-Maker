using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IDBToDBResReceiver
{
    Action<DBGetResult> GetInDbgrReceiver();
}

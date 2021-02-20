using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProRenameToDBSetSender
{
    void AddInDbsrReceiver(Action<DBSetRequest> action);
}

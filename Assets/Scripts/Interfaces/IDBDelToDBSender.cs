using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IDBDelToDBSender
{
    void AddOutDbdrReceiver(Action<DBDelRequest> action);
}

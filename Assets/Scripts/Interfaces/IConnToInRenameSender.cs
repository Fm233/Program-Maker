using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IRenameSender
{
    void AddRiReceiver(Action<RenameInf> action);
}

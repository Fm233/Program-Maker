using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInRenameToProRenameSender
{
    void AddPriReceiver(Action<RenameInf> action);
}

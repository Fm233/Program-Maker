using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProRenameToOutRenameSender
{
    void AddFriReceiver(Action<RenameInf> action);
}

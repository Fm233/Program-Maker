using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IConnToInRenameReceiver
{
    Action<RenameInf> GetDriReceiver();
}

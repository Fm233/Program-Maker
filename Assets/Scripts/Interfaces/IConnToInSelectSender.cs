using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface ISelectSender
{
    void AddSiReceiver(Action<SelectInf> action);
}

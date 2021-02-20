using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInSelectToExPanelSender
{
    void AddSiReceiver(Action<SelectInf> action);
}

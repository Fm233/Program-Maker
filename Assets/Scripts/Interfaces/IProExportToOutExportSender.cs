using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IProExportToOutExportSender
{
    void AddFeiReceiver(Action<FinalExportInf> action);
}

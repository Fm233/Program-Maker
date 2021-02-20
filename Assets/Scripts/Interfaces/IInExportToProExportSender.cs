using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInExportToProExportSender
{
    void AddPeiReceiver(Action<ExportInf> action);
}

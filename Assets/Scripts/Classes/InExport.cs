using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class InExport : MonoBehaviour, IInExportToProExportSender
{
    public InputField input;
    void Start()
    {
        input.text = "0";
    }
    void Update()
    {
        // Update here
    }
    public void Export()
    {
        peiAction(new ExportInf(int.Parse(input.text)));
    }
    Action<ExportInf> peiAction;
    public void AddPeiReceiver(Action<ExportInf> action)
    {
        peiAction += action;
    }
}

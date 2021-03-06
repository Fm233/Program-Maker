﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramInterface
{
    public bool isReceiver;
    public string type;
    public string param;

    public ProgramInterface(bool isReceiver, string type, string param)
    {
        this.isReceiver = isReceiver;
        this.type = type;
        this.param = param;
    }

    public virtual void AddInterfaceInformation(ref List<string> p)
    {
        if (isReceiver)
        {
            p.Add("    public void Receive" + Util.ToBigCamel(param) + "(" + type + " " + param + ")");
            p.Add("    {");
            p.Add("        // Fill receiver function here");
            p.Add("    }");
        }
        else
        {
            p.Add("    public Action<" + type + "> " + param + "Action { get; set; }");
        }
    }

    public void AddInterfaceInformation(ref List<string> p, string[] content)
    {
        if (isReceiver)
        {
            p.Add("    public void Receive" + Util.ToBigCamel(param) + "(" + type + " " + param + ")");
            p.Add("    {");
            foreach (string c in content)
            {
                p.Add(c);
            }
            p.Add("    }");
        }
        else
        {
            p.Add("    public Action<" + type + "> " + param + "Action { get; set; }");
        }
    }
}

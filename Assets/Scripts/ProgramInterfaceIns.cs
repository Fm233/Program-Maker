using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramInterfaceIns : ProgramInterface, IProgram
{
    public string interfaceName
    {
        get
        {
            return "I" + Util.ToBigCamel(param) + "Receiver";
        }
    }

    public ProgramInterfaceIns(string type, string param) : base(true, type, param)
    {
    }

    public void InitContent(ref List<string> p)
    {
        p.Add("public interface " + interfaceName);
        p.Add("{");
        p.Add("    void Receive" + Util.ToBigCamel(param) + "(" + type + " " + param + ");");
        p.Add("}");
    }
}

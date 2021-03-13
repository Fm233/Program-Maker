using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramInstance : ProgramClass
{
    public ProgramInstance(string name) : base(name)
    {
    }

    public override bool IsIMB()
    {
        return false;
    }

    public override bool NeedMBInstantiate()
    {
        return false;
    }

    public override void InitContent(ref List<string> p)
    {
        p.Add("public class " + className + ConvertInterfaces());
        p.Add("{");
        foreach (ProgramInterface programInterface in interfaces)
        {
            if (!programInterface.isReceiver)
            {
                programInterface.AddInterfaceInformation(ref p);
                p.Add("    ");
            }
        }
        p.Add("    public void Start()");
        p.Add("    {");
        p.Add("        // Start here");
        p.Add("    }");
        p.Add("    public void Update()");
        p.Add("    {");
        p.Add("        // Update here");
        p.Add("    }");
        foreach (ProgramInterface programInterface in interfaces)
        {
            if (programInterface.isReceiver)
            {
                p.Add("    ");
                programInterface.AddInterfaceInformation(ref p);
            }
        }
        p.Add("}");
    }
    string ConvertInterfaces()
    {
        string result = " : MonoBehaviour";
        for (int i = 0; i < interfaces.Count; i++)
        {
            ProgramInterface a = interfaces[i];
            if (a is ProgramInterfaceIns)
            {
                ProgramInterfaceIns b = (ProgramInterfaceIns)a;
                result += ", ";
                result += b.interfaceName;
            }
        }
        return result;
    }
}

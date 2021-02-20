using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramClass : IProgram
{
    public string className;

    List<ProgramInterface> interfaces = new List<ProgramInterface>();

    public ProgramClass(string name)
    {
        this.className = name;
    }

    public void AddInterface(ProgramInterface programInterface)
    {
        interfaces.Add(programInterface);
    }

    public void InitContent(ref List<string> p)
    {
        p.Add("public class " + className);
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

    /*
    string ConvertInterfaces()
    {
        string result = "";
        for (int i = 0; i < interfaces.Count; i++)
        {
            result += ", ";
            ProgramInterface i1 = (ProgramInterface)interfaces[i];
            result += i1.interfaceName;
        }
        return result;
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramClass : IProgram
{
    public string className;
    protected string overrideName = "";
    public string insName
    {
        get
        {
            if (overrideName.Length > 0)
            {
                return overrideName;
            }
            return Util.ToSmallCamel(className);
        }
    }

    protected List<ProgramInterface> interfaces = new List<ProgramInterface>();

    public ProgramClass(string name)
    {
        this.className = name;
    }

    public virtual bool IsIMB()
    {
        return true;
    }

    public virtual bool NeedMBInstantiate()
    {
        return false;
    }

    public void OverrideName(string name)
    {
        overrideName = name;
    }

    public void AddInterface(ProgramInterface programInterface)
    {
        interfaces.Add(programInterface);
    }

    public virtual void InitContent(ref List<string> p)
    {
        p.Add("public class " + className + " : IMB");
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

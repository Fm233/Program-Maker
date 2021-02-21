using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramClassDB : ProgramClass
{
    string cname
    {
        get
        {
            return className.Substring(2);
        }
    }

    public ProgramClassDB(string name) : base(name)
    {
    }

    public override void InitContent(ref List<string> p)
    {
        p.Add("public class " + className + " : IMB");
        p.Add("{");
        p.Add("List<" + cname + "> elements = new List<" + cname + ">();");
        foreach (ProgramInterface programInterface in interfaces)
        {
            if (programInterface.isReceiver)
            {
                p.Add("    ");
                List<string> content = new List<string>();
                if (programInterface.type.Contains("Get"))
                {
                    content.Add("		List<" + cname + "> res = new List<" + cname + ">();");
                    content.Add("		foreach (" + cname + " e in elements)");
                    content.Add("       {");
                    content.Add("			if (sel(e))");
                    content.Add("           {");
                    content.Add("				res.Add(e);");
                    content.Add("			}");
                    content.Add("		}");
                    content.Add("		ret(res);");
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
                if (programInterface.type.Contains("Set"))
                {
                    content.Add("		elements = vals;");
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
                if (programInterface.type.Contains("Mod"))
                {
                    content.Add("		foreach (cname e in elements)");
                    content.Add("		{");
                    content.Add("			if (sel(e))");
                    content.Add("			{");
                    content.Add("				mod(e);");
                    content.Add("			}");
                    content.Add("		}");
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
                if (programInterface.type.Contains("Del"))
                {
                    content.Add("		elements.RemoveAll(sel);");
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
                if (programInterface.type.Contains("Crt"))
                {
                    content.Add("		elements.Add(val);");
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
                if (programInterface.type.Contains("Fnd"))
                {
                    content.Add("		ret(elements);");
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
            }
        }
        p.Add("}");
    }
}
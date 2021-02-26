using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramClassDB : ProgramClass
{
    public TypeDB t;

    string cname
    {
        get
        {
            if (t == TypeDB.FC)
            {
                return "Ins" + className.Substring(2);
            }
            return className.Substring(2);
        }
    }

    public ProgramClassDB(string name, TypeDB t) : base(name)
    {
        this.t = t;
    }

    public override void InitContent(ref List<string> p)
    {
        p.Add("public class " + className + " : IMB");
        p.Add("{");
        if (t == TypeDB.DS)
        {
            p.Add("    " + cname + " element;");
            foreach (ProgramInterface programInterface in interfaces)
            {
                if (programInterface.isReceiver)
                {
                    p.Add("    ");
                    List<string> content = new List<string>();
                    if (programInterface.type.Contains("Get"))
                    {
                        content.Add("		ret(element);");
                    }
                    if (programInterface.type.Contains("Set"))
                    {
                        content.Add("		element = val;");
                    }
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
            }
        }
        if (t == TypeDB.DB)
        {
            p.Add("    List<" + cname + "> elements = new List<" + cname + ">();");
            p.Add("    public GameObject prefab;");
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
                    }
                    if (programInterface.type.Contains("Set"))
                    {
                        content.Add("		elements = vals;");
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
                    }
                    if (programInterface.type.Contains("Del"))
                    {
                        content.Add("		elements.RemoveAll(sel);");
                    }
                    if (programInterface.type.Contains("Crt"))
                    {
                        content.Add("		elements.Add(val);");
                    }
                    if (programInterface.type.Contains("Fnd"))
                    {
                        content.Add("		ret(elements);");
                    }
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
            }
        }
        if (t == TypeDB.FC)
        {
            p.Add("    List<" + cname + "> elements = new List<" + cname + ">();");
            p.Add("    public GameObject prefab;");
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
                    }
                    if (programInterface.type.Contains("Del"))
                    {
                        content.Add("		List<Line> toDelete = new List<Line>();");
                        content.Add("		foreach (Line e in elements)");
                        content.Add("		{");
                        content.Add("			if (sel(e))");
                        content.Add("			{");
                        content.Add("				toDelete.Add(e);");
                        content.Add("			}");
                        content.Add("		}");
                        content.Add("		foreach (Line d in toDelete)");
                        content.Add("		{");
                        content.Add("			elements.Remove(d);");
                        content.Add("			d.gameObject.SetActive(false); // TODO Destroy");
                        content.Add("		}");
                    }
                    if (programInterface.type.Contains("Crt"))
                    {
                        content.Add("		GameObject instance = Instantiate(prefab);");
                        content.Add("		Line comp = instance.GetComponent<Line>();");
                        content.Add("		elements.Add(comp);");
                        content.Add("		ret(comp);");
                    }
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
            }
        }
        p.Add("}");
    }
}

public enum TypeDB
{
    DS,
    DB,
    FC
}
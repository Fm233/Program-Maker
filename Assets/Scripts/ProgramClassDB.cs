using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramClassDB : ProgramClass
{
    List<ProgramInterface> sendInterfaces = new List<ProgramInterface>();
    List<ProgramInterface> receiveInterfaces = new List<ProgramInterface>();
    List<ProgramClass> sendClasses = new List<ProgramClass>();
    List<ProgramClass> receiveClasses = new List<ProgramClass>();

    public TypeDB t;

    public string cname
    {
        get
        {
            if (t == TypeDB.FC)
            {
                return "Ins" + className.Substring(3);
            }
            return className.Substring(2);
        }
    }

    public override bool IsIMB()
    {
        return false;
    }

    public override bool NeedMBInstantiate()
    {
        return t == TypeDB.FC;
    }

    public List<ProgramClass> GetReceiveClasses()
    {
        return receiveClasses;
    }

    public ProgramClassDB(string name, TypeDB t) : base(name)
    {
        this.t = t;
    }

    public void AddConnection(ProgramInterface sendInterface,
                              ProgramInterface receiveInterface,
                              ProgramClass sendClass,
                              ProgramClass receiveClass)
    {
        sendInterfaces.Add(sendInterface);
        receiveInterfaces.Add(receiveInterface);
        sendClasses.Add(sendClass);
        receiveClasses.Add(receiveClass);
    }

    public override void InitContent(ref List<string> p)
    {
        if (t == TypeDB.DS)
        {
            p.Add("public class " + className);
            p.Add("{");
            p.Add("    " + cname + " element;");
            foreach (ProgramInterface programInterface in interfaces)
            {
                if (programInterface.isReceiver)
                {
                    p.Add("    ");
                    List<string> content = new List<string>();
                    if (programInterface.type.Contains("Get"))
                    {
                        content.Add("        " + programInterface.param + ".ret(element);");
                    }
                    if (programInterface.type.Contains("Set"))
                    {
                        content.Add("        element = " + programInterface.param + ".val;");
                    }
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
            }
        }
        if (t == TypeDB.DB)
        {
            p.Add("public class " + className);
            p.Add("{");
            p.Add("    List<" + cname + "> elements = new List<" + cname + ">();");
            foreach (ProgramInterface programInterface in interfaces)
            {
                if (programInterface.isReceiver)
                {
                    p.Add("    ");
                    List<string> content = new List<string>();
                    if (programInterface.type.Contains("Get"))
                    {
                        content.Add("        List<" + cname + "> res = new List<" + cname + ">();");
                        content.Add("        foreach (" + cname + " e in elements)");
                        content.Add("        {");
                        content.Add("            if (" + programInterface.param + ".sel(e))");
                        content.Add("           {");
                        content.Add("                res.Add(e);");
                        content.Add("            }");
                        content.Add("        }");
                        content.Add("        " + programInterface.param + ".ret(res);");
                    }
                    if (programInterface.type.Contains("Set"))
                    {
                        content.Add("        elements = " + programInterface.param + ".vals;");
                    }
                    if (programInterface.type.Substring(5).Contains("Mod"))
                    {
                        content.Add("        foreach (" + cname + " e in elements)");
                        content.Add("        {");
                        content.Add("            if (" + programInterface.param + ".sel(e))");
                        content.Add("            {");
                        content.Add("                " + programInterface.param + ".mod(e);");
                        content.Add("            }");
                        content.Add("        }");
                    }
                    if (programInterface.type.Contains("Del"))
                    {
                        content.Add("        elements.RemoveAll(" + programInterface.param + ".sel);");
                    }
                    if (programInterface.type.Contains("Crt"))
                    {
                        content.Add("        elements.Add(" + programInterface.param + ".val);");
                    }
                    if (programInterface.type.Contains("Fnd"))
                    {
                        content.Add("        " + programInterface.param + ".ret(elements);");
                    }
                    programInterface.AddInterfaceInformation(ref p, content.ToArray());
                }
            }
        }
        if (t == TypeDB.FC)
        {
            p.Add("public class " + className + " : MonoBehaviour");
            p.Add("{");
            p.Add("    List<" + cname + "> elements = new List<" + cname + ">();");
            p.Add("    public GameObject prefab;");
            foreach (ProgramClass recv in receiveClasses)
            {
                p.Add("    public " + recv.className + " " + recv.insName + ";");
            }
            foreach (ProgramInterface programInterface in interfaces)
            {
                if (programInterface.isReceiver)
                {
                    p.Add("    ");
                    List<string> content = new List<string>();
                    if (programInterface.type.Contains("Get"))
                    {
                        content.Add("        List<" + cname + "> res = new List<" + cname + ">();");
                        content.Add("        foreach (" + cname + " e in elements)");
                        content.Add("        {");
                        content.Add("            if (" + programInterface.param + ".sel(e))");
                        content.Add("           {");
                        content.Add("                res.Add(e);");
                        content.Add("            }");
                        content.Add("        }");
                        content.Add("        " + programInterface.param + ".ret(res);");
                    }
                    if (programInterface.type.Contains("Del"))
                    {
                        content.Add("        List<" + cname + "> toDelete = new List<" + cname + ">();");
                        content.Add("        foreach (" + cname + " e in elements)");
                        content.Add("        {");
                        content.Add("            if (" + programInterface.param + ".sel(e))");
                        content.Add("            {");
                        content.Add("                toDelete.Add(e);");
                        content.Add("            }");
                        content.Add("        }");
                        content.Add("        foreach (" + cname + " d in toDelete)");
                        content.Add("        {");
                        content.Add("            elements.Remove(d);");
                        content.Add("            d.gameObject.SetActive(false); // TODO Destroy");
                        content.Add("        }");
                    }
                    if (programInterface.type.Contains("Crt"))
                    {
                        content.Add("        GameObject instance = Instantiate(prefab);");
                        content.Add("        " + cname + " comp = instance.GetComponent<" + cname + ">();");
                        content.Add("        elements.Add(comp);");
                        for (int i = 0; i < sendInterfaces.Count; i++)
                        {
                            string rcname = receiveClasses[i].insName;
                            string param = Util.ToBigCamel(sendInterfaces[i].param);
                            content.Add("        comp." + sendInterfaces[i].param + "Action += " + rcname + ".Receive" + param + ";");
                        }
                        content.Add("        " + programInterface.param + ".ret(comp);");
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramMainClass : IProgram
{
    List<ProgramInterface> sendInterfaces = new List<ProgramInterface>();
    List<ProgramInterface> receiveInterfaces = new List<ProgramInterface>();
    List<string> sendClasses = new List<string>();
    List<string> receiveClasses = new List<string>();
    List<string> featuredClasses = new List<string>();

    public void AddConnection(ProgramInterface sendInterface,
                              ProgramInterface receiveInterface,
                              string sendClass,
                              string receiveClass)
    {
        sendInterfaces.Add(sendInterface);
        receiveInterfaces.Add(receiveInterface);
        sendClasses.Add(sendClass);
        receiveClasses.Add(receiveClass);
        if (!featuredClasses.Contains(sendClass))
        {
            featuredClasses.Add(sendClass);
        }
        if (!featuredClasses.Contains(receiveClass))
        {
            featuredClasses.Add(receiveClass);
        }
    }

    public void InitContent(ref List<string> p)
    {
        p.Add("public class Main : MonoBehaviour");
        p.Add("{");
        for (int i = 0; i < featuredClasses.Count; i++)
        {
            string ctype = featuredClasses[i];
            string cname = Util.ToSmallCamel(ctype);
            p.Add("    public " + ctype + " " + cname + ";");
        }
        p.Add("    void Start()");
        p.Add("    {");
        for (int i = 0; i < sendInterfaces.Count; i++)
        {
            string scname = Util.ToSmallCamel(sendClasses[i]);
            string rcname = Util.ToSmallCamel(receiveClasses[i]);
            string param = Util.ToBigCamel(sendInterfaces[i].param);
            p.Add("        " + scname + "." + sendInterfaces[i].param + "Action += " + rcname + ".Receive" + param + ";");
        }
        p.Add("    }");
        p.Add("}");
    }
}

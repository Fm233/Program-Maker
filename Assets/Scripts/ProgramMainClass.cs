using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramMainClass : IProgram
{
    List<ProgramInterface> sendInterfaces = new List<ProgramInterface>();
    List<ProgramInterface> receiveInterfaces = new List<ProgramInterface>();
    List<ProgramClass> sendClasses = new List<ProgramClass>();
    List<ProgramClass> receiveClasses = new List<ProgramClass>();
    List<ProgramClass> featuredClasses = new List<ProgramClass>();

    public void AddConnection(ProgramInterface sendInterface,
                              ProgramInterface receiveInterface,
                              ProgramClass sendClass,
                              ProgramClass receiveClass)
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
        foreach (ProgramClass c in featuredClasses)
        {
            string ctype = c.className;
            string cname = c.insName;
            p.Add("    public " + ctype + " " + cname + ";");
        }
        p.Add("    public Updater updater;");
        p.Add("    void Start()");
        p.Add("    {");
        p.Add("        // Update");
        foreach (ProgramClass c in featuredClasses)
        {
            string ctype = c.className;
            string cname = c.insName;
            p.Add("        updater.AddIMB(" + cname + ");");
        }
        p.Add("        ");
        p.Add("        // Connect");
        for (int i = 0; i < sendInterfaces.Count; i++)
        {
            string scname = sendClasses[i].insName;
            string rcname = receiveClasses[i].insName;
            string param = Util.ToBigCamel(sendInterfaces[i].param);
            p.Add("        " + scname + "." + sendInterfaces[i].param + "Action += " + rcname + ".Receive" + param + ";");
        }
        p.Add("    }");
        p.Add("}");
    }
}

using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramUpdater : IProgram
{
    public void InitContent(ref List<string> p)
    {
        p.Add("public class Updater : MonoBehaviour");
        p.Add("{");
        p.Add("    List<IMB> imbs = new List<IMB>();");
        p.Add("    public void AddIMB(IMB imb)");
        p.Add("    {");
        p.Add("        imbs.Add(imb);");
        p.Add("    }");
        p.Add("    private void Start()");
        p.Add("    {");
        p.Add("        foreach (IMB imb in imbs)");
        p.Add("        {");
        p.Add("            imb.Start();");
        p.Add("        }");
        p.Add("    }");
        p.Add("    private void Update()");
        p.Add("    {");
        p.Add("        foreach (IMB imb in imbs)");
        p.Add("        {");
        p.Add("            imb.Update();");
        p.Add("        }");
        p.Add("    }");
        p.Add("}");
    }
}

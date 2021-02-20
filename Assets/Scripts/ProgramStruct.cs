using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramStruct : IProgram
{
    public string structName;
    List<StructPair> pairs = new List<StructPair>();

    public ProgramStruct(string name)
    {
        this.structName = name;
    }

    public void AddPair(string a, string b)
    {
        pairs.Add(new StructPair(a, b));
    }

    public void InitContent(ref List<string> p)
    {
        p.Add("public struct " + structName);
        p.Add("{");
        foreach (StructPair pair in pairs)
        {
            p.Add("    public " + pair.a + " " + pair.b);
        }
        p.Add("");
        string ctor = "    public " + structName + "(";
        for (int i = 0; i < pairs.Count; i++)
        {
            StructPair pair = pairs[i];
            ctor += pair.a + " " + pair.b;
            if (i + 1 != pairs.Count)
            {
                ctor += ", ";
            }
        }
        ctor += ")";
        p.Add(ctor);
        p.Add("    {");
        foreach (StructPair pair in pairs)
        {
            p.Add("        this." + pair.b + " = " + pair.b);
        }
        p.Add("    }");
        p.Add("}");
    }
}

public struct StructPair
{
    public string a;
    public string b;

    public StructPair(string a, string b)
    {
        this.a = a;
        this.b = b;
    }
}
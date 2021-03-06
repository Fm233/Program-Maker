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
        StructPair sp = new StructPair(a, b);
        if (!pairs.Contains(sp))
        {
            pairs.Add(sp);
        }
    }

    public void InitContent(ref List<string> p)
    {
        p.Add("public class " + structName);
        p.Add("{");
        foreach (StructPair pair in pairs)
        {
            p.Add("    public " + pair.a + " " + pair.b + ";");
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
            p.Add("        this." + pair.b + " = " + pair.b + ";");
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

    public override bool Equals(object obj)
    {
        return obj is StructPair pair &&
               a == pair.a &&
               b == pair.b;
    }

    public override int GetHashCode()
    {
        int hashCode = 2118541809;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(a);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(b);
        return hashCode;
    }
}
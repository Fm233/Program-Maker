using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramEnum : IProgram
{
    public string enumName;
    List<string> enums = new List<string>();

    public ProgramEnum(string enumName)
    {
        this.enumName = enumName;
    }

    public void AddEnum(string e)
    {
        enums.Add(e);
    }

    public void InitContent(ref List<string> p)
    {
        p.Add("public enum " + enumName);
        p.Add("{");
        for (int i = 0; i < enums.Count; i++)
        {
            string e = enums[i];
            if (i + 1 == enums.Count)
            {
                p.Add("    " + e);
            }
            else
            {
                p.Add("    " + e + ",");
            }
        }
        p.Add("}");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramIMB : IProgram
{
    public void InitContent(ref List<string> p)
    {
        p.Add("public interface IMB");
        p.Add("{");
        p.Add("    void Start();");
        p.Add("    void Update();");
        p.Add("}");
    }
}
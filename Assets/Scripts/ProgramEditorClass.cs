using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramEditorClass : IProgram
{
    List<string> featuredClasses = new List<string>();
    public void AddClass(string inClass)
    {
        if (!featuredClasses.Contains(inClass))
        {
            featuredClasses.Add(inClass);
        }
    }
    public void InitContent(ref List<string> p)
    {
        p.Add("#if UNITY_EDITOR");
        p.Add("using System.Collections;");
        p.Add("using System.Collections.Generic;");
        p.Add("using System;");
        p.Add("using UnityEngine;");
        p.Add("using UnityEditor;");
        p.Add("public class InitMan : Editor");
        p.Add("{");
        p.Add("    [MenuItem(\"InitMan/Init\")]");
        p.Add("    public static void Init()");
        p.Add("    {");
        p.Add("        GameObject mainObject = new GameObject();");
        p.Add("        mainObject.name = \"Main\";");
        p.Add("        Main main = mainObject.AddComponent<Main>();");
        foreach (string c in featuredClasses)
        {
            string objName = Util.ToSmallCamel(c);
            p.Add("        GameObject " + objName + " = new GameObject();");
            p.Add("        " + objName + ".name = \"" + c + " Instance\";");
            p.Add("        main." + objName + " = " + objName + ".AddComponent<" + c + ">();");
        }
        p.Add("    }");
        p.Add("}");
        p.Add("#endif");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramEditorClass : IProgram
{
    List<ProgramClass> featuredClasses = new List<ProgramClass>();
    public void AddClass(ProgramClass inClass)
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

        p.Add("        // Create classes");
        p.Add("        GameObject mainObject = new GameObject();");
        p.Add("        mainObject.name = \"Main\";");
        p.Add("        Main main = mainObject.AddComponent<Main>();");
        p.Add("        GameObject updater = new GameObject();");
        p.Add("        updater.name = \"Updater\";");
        p.Add("        main.updater = updater.AddComponent<Updater>();");
        List<ProgramClassDB> factories = new List<ProgramClassDB>();
        foreach (ProgramClass c in featuredClasses)
        {
            string objName = c.insName;
            p.Add("        GameObject " + objName + " = new GameObject();");
            p.Add("        " + objName + ".name = \"" + c.className + "\";");
            p.Add("        main." + objName + " = " + objName + ".AddComponent<" + c.className + ">();");
            if (c is ProgramClassDB)
            {
                ProgramClassDB db = (ProgramClassDB)c;
                if (db.t == TypeDB.FC)
                {
                    // Allocate factory
                    factories.Add(db);
                }
            }
        }

        p.Add("        ");
        p.Add("        // Link factories");
        foreach (ProgramClassDB fac in factories)
        {
            string objName = fac.insName;
            foreach (ProgramClass cls in fac.GetReceiveClasses())
            {
                string clobj = cls.insName;
                p.Add("        main." + objName + "." + clobj + " = main." + clobj + ";");
            }
        }
        p.Add("    }");
        p.Add("}");
        p.Add("#endif");
    }
}

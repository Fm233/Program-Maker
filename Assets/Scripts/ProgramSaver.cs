using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
public static class ProgramSaver
{
    public static string scriptsPath { get; set; } = Application.persistentDataPath;

    public static void SaveProgram(string root, string name, List<string> content, bool overwrite = false)
    {
        string path = scriptsPath + "/" + root;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string dir = path + "/" + name + ".cs";
        if (overwrite)
        {
            File.WriteAllLines(dir, content.ToArray());
        }
        else
        {
            if (File.Exists(dir))
            {
                string original = File.ReadAllText(dir);
                string[] originalLines = File.ReadAllLines(dir);
                List<string> filtered = new List<string>();
                bool bypass = false;
                foreach (string c in content)
                {
                    if (c.Contains("    public ") && original.Contains(c))
                    {
                        bypass = true;
                    }
                    if (c.Trim().Length == 0 || c == "}")
                    {
                        bypass = false;
                    }
                    if (!bypass)
                    {
                        filtered.Add(c);
                    }
                    if (c == "{")
                    {
                        bool flag = false;
                        foreach (string o in originalLines)
                        {
                            if (o == "}")
                            {
                                flag = false;
                            }
                            if (flag)
                            {
                                filtered.Add(o);
                            }
                            if (o == "{")
                            {
                                flag = true;
                            }
                        }
                    }
                }
                File.WriteAllLines(dir, filtered.ToArray());
            }
            else
            {
                File.WriteAllLines(dir, content.ToArray());
            }
        }
    }

    public static void SaveClass(string root, ProgramClass programClass)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programClass.InitContent(ref program);
        bool overwrite = false;
        string className = programClass.className;
        if (className.StartsWith("Fac") || className.StartsWith("DS") || className.StartsWith("DB"))
        {
            overwrite = true;
        }
        SaveProgram(root, className, program, overwrite);
    }

    public static void SaveMainClass(string root, ProgramMainClass programClass)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programClass.InitContent(ref program);
        SaveProgram(root, "Main", program, true);
    }

    public static void SaveEditorClass(string root, ProgramEditorClass programClass)
    {
        List<string> program = new List<string>();
        programClass.InitContent(ref program);
        SaveProgram(root, "InitMan", program, true);
    }

    public static void SaveStruct(string root, ProgramStruct programStruct)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programStruct.InitContent(ref program);
        SaveProgram(root, programStruct.structName, program, true);
    }

    public static void SaveInterface(string root, ProgramInterfaceIns programInterface)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programInterface.InitContent(ref program);
        SaveProgram(root, programInterface.interfaceName, program, true);
    }

    public static void SaveEnum(string root, ProgramEnum programEnum)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programEnum.InitContent(ref program);
        SaveProgram(root, programEnum.enumName, program, true);
    }

    public static void SaveUpdater(string root)
    {
        ProgramUpdater updater = new ProgramUpdater();
        List<string> program = new List<string>();
        AddUsings(ref program);
        updater.InitContent(ref program);
        SaveProgram(root, "Updater", program, true);
    }

    public static void SaveIMB(string root)
    {
        ProgramIMB imb = new ProgramIMB();
        List<string> program = new List<string>();
        AddUsings(ref program);
        imb.InitContent(ref program);
        SaveProgram(root, "IMB", program, true);
    }

    static void AddUsings(ref List<string> p)
    {
        p.Add("using System.Collections;");
        p.Add("using System.Collections.Generic;");
        p.Add("using System;");
        p.Add("using UnityEngine;");
    }
}

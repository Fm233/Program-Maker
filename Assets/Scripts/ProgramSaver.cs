using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public enum OverwriteType
{
    OVERWRITE,
    APPEND,
    NO
}

public static class ProgramSaver
{
    public static string scriptsPath { get; set; } = Application.persistentDataPath;

    public static void SaveProgram(string root, string name, List<string> content, OverwriteType overwrite = OverwriteType.APPEND)
    {
        string path = scriptsPath + "/" + root;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string dir = path + "/" + name + ".cs";
        if (overwrite == OverwriteType.OVERWRITE)
        {
            File.WriteAllLines(dir, content.ToArray());
        }
        else
        {
            if (overwrite == OverwriteType.NO)
            {
                if (!File.Exists(dir))
                {
                    File.WriteAllLines(dir, content.ToArray());
                }
            }
            else
            {
                if (File.Exists(dir))
                {
                    // Add usings
                    string original = File.ReadAllText(dir);
                    string[] originalLines = File.ReadAllLines(dir);
                    List<string> filtered = new List<string>();
                    bool bypass = false;
                    foreach (string o in originalLines)
                    {
                        if (o.Contains("using"))
                        {
                            filtered.Add(o);
                        }
                    }

                    // Add content
                    foreach (string c in content)
                    {
                        if (c.Contains("using"))
                        {
                            continue;
                        }
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
                            if (c.Trim().Length > 0 || filtered[filtered.Count - 1].Trim().Length > 0)
                            {
                                filtered.Add(c);
                            }
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
                                    if (o.Trim().Length > 0 || filtered[filtered.Count - 1].Trim().Length > 0)
                                    {
                                        filtered.Add(o);
                                    }
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
    }

    public static void SaveClass(string root, ProgramClass programClass)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programClass.InitContent(ref program);
        OverwriteType overwrite = OverwriteType.APPEND;
        string className = programClass.className;
        if (className.StartsWith("DS") || className.StartsWith("DB"))
        {
            overwrite = OverwriteType.NO;
        }
        if (className.StartsWith("Fac"))
        {
            overwrite = OverwriteType.OVERWRITE;
        }
        SaveProgram(root, className, program, overwrite);
    }

    public static void SaveMainClass(string root, ProgramMainClass programClass)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programClass.InitContent(ref program);
        SaveProgram(root, "Main", program, OverwriteType.OVERWRITE);
    }

    public static void SaveEditorClass(string root, ProgramEditorClass programClass)
    {
        List<string> program = new List<string>();
        programClass.InitContent(ref program);
        SaveProgram(root, "InitMan", program, OverwriteType.OVERWRITE);
    }

    public static void SaveStruct(string root, ProgramStruct programStruct)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programStruct.InitContent(ref program);
        SaveProgram(root, programStruct.structName, program, OverwriteType.OVERWRITE);
    }

    public static void SaveInterface(string root, ProgramInterfaceIns programInterface)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programInterface.InitContent(ref program);
        SaveProgram(root, programInterface.interfaceName, program, OverwriteType.OVERWRITE);
    }

    public static void SaveEnum(string root, ProgramEnum programEnum)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programEnum.InitContent(ref program);
        SaveProgram(root, programEnum.enumName, program, OverwriteType.OVERWRITE);
    }

    public static void SaveUpdater(string root)
    {
        ProgramUpdater updater = new ProgramUpdater();
        List<string> program = new List<string>();
        AddUsings(ref program);
        updater.InitContent(ref program);
        SaveProgram(root, "Updater", program, OverwriteType.OVERWRITE);
    }

    public static void SaveIMB(string root)
    {
        ProgramIMB imb = new ProgramIMB();
        List<string> program = new List<string>();
        AddUsings(ref program);
        imb.InitContent(ref program);
        SaveProgram(root, "IMB", program, OverwriteType.OVERWRITE);
    }

    static void AddUsings(ref List<string> p)
    {
        p.Add("using System.Collections;");
        p.Add("using System.Collections.Generic;");
        p.Add("using System;");
        p.Add("using UnityEngine;");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
public static class ProgramSaver
{
    public static void SaveProgram(string root, string name, string content)
    {
        string path = Application.persistentDataPath + "/" + root;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string dir = path + "/" + name + ".cs";
        File.WriteAllText(dir, content);
    }
    public static void SaveProgram(string root, string name, List<string> contentList)
    {
        string content = "";
        foreach (string str in contentList)
        {
            content += str;
            content += "\n";
        }
        SaveProgram(root, name, content);
    }

    public static void SaveClass(string root, ProgramClass programClass)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programClass.InitContent(ref program);
        SaveProgram(root, programClass.className, program);
    }

    public static void SaveMainClass(string root, ProgramMainClass programClass)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programClass.InitContent(ref program);
        SaveProgram(root, "Main", program);
    }

    public static void SaveEditorClass(string root, ProgramEditorClass programClass)
    {
        List<string> program = new List<string>();
        programClass.InitContent(ref program);
        SaveProgram(root, "InitMan", program);
    }

    public static void SaveStruct(string root, ProgramStruct programStruct)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programStruct.InitContent(ref program);
        SaveProgram(root, programStruct.structName, program);
    }

    public static void SaveInterface(string root, ProgramInterfaceIns programInterface)
    {
        List<string> program = new List<string>();
        AddUsings(ref program);
        programInterface.InitContent(ref program);
        SaveProgram(root, programInterface.interfaceName, program);
    }

    static void AddUsings(ref List<string> p)
    {
        p.Add("using System.Collections;");
        p.Add("using System.Collections.Generic;");
        p.Add("using System;");
        p.Add("using UnityEngine;");
    }
}

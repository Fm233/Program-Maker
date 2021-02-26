using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class OutExport : MonoBehaviour, IProExportToOutExportReceiver
{
    public InputField input;
    List<string> definedStructs = new List<string>()
    {
            "int",
            "Vector3",
            "string",
            "float",
            "double",
            "char",
            "bool",
            "Vector2",
            "Touch"
    };
    List<IElementModel> elements;
    List<ProgramStruct> structs = new List<ProgramStruct>();
    List<ProgramEnum> enums = new List<ProgramEnum>();
    List<ProgramClass> classes = new List<ProgramClass>();

    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<FinalExportInf> GetFeiReceiver()
    {
        return ReceiveFei;
    }
    public void ReceiveFei(FinalExportInf fei)
    {
        Export(fei);
    }

    void Export(FinalExportInf fei)
    {
        // Init values
        elements = fei.models;
        RevertDefinedStructs();
        int buildCount = fei.buildCount;

        // Init classes
        classes.Clear();
        structs.Clear();
        enums.Clear();
        ProgramMainClass mainClass = new ProgramMainClass();
        ProgramEditorClass editorClass = new ProgramEditorClass();
        foreach (IElementModel element in elements)
        {
            if (element is NodeModel)
            {
                NodeModel node = (NodeModel)element;
                string insName = NodeToName(node);
                ProgramClass c = new ProgramClass(node.name);
                if (insName != node.name)
                {
                    c.OverrideName(insName);
                }
                classes.Add(c);
            }
        }

        // Init interfaces and structs
        foreach (IElementModel element in elements)
        {
            if (element is InterModel)
            {
                // Register conns
                List<ConnModel> ins = new List<ConnModel>();
                List<ConnModel> outs = new List<ConnModel>();
                List<ConnModel> all = new List<ConnModel>();
                foreach (IElementModel e in elements)
                {
                    if (e is ConnModel)
                    {
                        ConnModel conn = (ConnModel)e;
                        if (conn.connInf.start == element.GetSelectInf())
                        {
                            outs.Add(conn);
                            all.Add(conn);
                        }
                        if (conn.connInf.end == element.GetSelectInf())
                        {
                            ins.Add(conn);
                            all.Add(conn);
                        }
                    }
                }

                // Get type and name
                string stype = "";
                string sname = "";
                foreach (ConnModel conn in all)
                {
                    string content = GetConnContent(conn);
                    if (content.Length > 0)
                    {
                        string nodename = "";
                        IElementModel start = Select(conn.connInf.start);
                        IElementModel end = Select(conn.connInf.end);
                        if (start is NodeModel)
                        {
                            nodename = ((NodeModel)start).name;
                        }
                        if (end is NodeModel)
                        {
                            nodename = ((NodeModel)end).name;
                        }
                        FullParseBracket(ref stype, content, nodename);
                        sname = stype.ToLower();
                        break;
                    }
                }

                // Generate
                ProgramInterface receiver = new ProgramInterface(true, stype, sname);
                ProgramInterface sender = new ProgramInterface(false, stype, sname);
                if (ins.Count > 1)
                {
                    NodeModel nodeEnd = (NodeModel)Select(outs[0].connInf.end);
                    foreach (ProgramClass pc in classes)
                    {
                        if (pc.className == nodeEnd.name)
                        {
                            pc.AddInterface(receiver);
                        }
                    }
                    foreach (ConnModel conn in ins)
                    {
                        NodeModel node = (NodeModel)Select(conn.connInf.start);
                        foreach (ProgramClass pc in classes)
                        {
                            if (pc.className == node.name)
                            {
                                pc.AddInterface(sender);
                            }
                        }
                        mainClass.AddConnection(sender, receiver, GetClass(node), GetClass(nodeEnd));
                    }
                }
                else
                {
                    NodeModel nodeStart = (NodeModel)Select(ins[0].connInf.start);
                    foreach (ProgramClass pc in classes)
                    {
                        if (pc.className == nodeStart.name)
                        {
                            pc.AddInterface(sender);
                        }
                    }
                    foreach (ConnModel conn in outs)
                    {
                        NodeModel node = (NodeModel)Select(conn.connInf.end);
                        foreach (ProgramClass pc in classes)
                        {
                            if (pc.className == node.name)
                            {
                                pc.AddInterface(receiver);
                            }
                        }
                        mainClass.AddConnection(sender, receiver, GetClass(nodeStart), GetClass(node));
                    }
                }
            }
            if (element is ConnModel)
            {
                // Init model
                ConnModel conn = (ConnModel)element;

                if (conn.connInf.start.type == ElementType.NODE && conn.connInf.end.type == ElementType.NODE)
                {
                    // Init conn models
                    NodeModel nodea = (NodeModel)Select(conn.connInf.start);
                    NodeModel nodeb = (NodeModel)Select(conn.connInf.end);

                    // Create interfaces
                    string content = GetConnContent(conn);
                    string stype = "";
                    string sname = "";
                    FullParseBracket(ref stype, content, nodea.name);
                    sname = stype.ToLower();

                    ProgramInterface sender = new ProgramInterface(false, stype, sname);
                    ProgramInterface receiver = nodeb.name.StartsWith("Ins") ?
                                                new ProgramInterfaceIns(stype, sname) :
                                                new ProgramInterface(true, stype, sname);
                    if (receiver is ProgramInterfaceIns)
                    {
                        ProgramSaver.SaveInterface("Build_" + buildCount.ToString() + "/Interfaces",
                                                   (ProgramInterfaceIns)receiver);
                    }
                    foreach (ProgramClass pc in classes)
                    {
                        if (pc.className == nodea.name)
                        {
                            pc.AddInterface(sender);
                        }
                        if (pc.className == nodeb.name)
                        {
                            pc.AddInterface(receiver);
                        }
                    }
                    mainClass.AddConnection(sender, receiver, GetClass(nodea), GetClass(nodeb));
                }
            }
        }

        // Save program
        for (int i = 0; i < structs.Count; i++)
        {
            if (!ContainInDefinedStructs(structs[i].structName))
            {
                ProgramSaver.SaveStruct("Build_" + buildCount.ToString() + "/Structs", structs[i]);
                definedStructs.Add(structs[i].structName);
            }
        }
        List<string> savedClasses = new List<string>();
        for (int i = 0; i < classes.Count; i++)
        {
            ProgramClass pc = classes[i];
            if (!savedClasses.Contains(pc.className))
            {
                ProgramSaver.SaveClass("Build_" + buildCount.ToString() + "/Classes", pc);
                editorClass.AddClass(pc.className);
                savedClasses.Add(pc.className);
            }
        }
        ProgramSaver.SaveMainClass("Build_" + buildCount.ToString() + "/Main", mainClass);
        ProgramSaver.SaveEditorClass("Build_" + buildCount.ToString() + "/Others", editorClass);

        // Change input text
        input.text = (buildCount + 1).ToString();
    }

    string GetConnContent(ConnModel conn)
    {
        // Auto content according to end
        IElementModel end = Select(conn.connInf.end);
        if (end is NodeModel)
        {
            NodeModel node = (NodeModel)end;
            string nname = node.name.Split(' ')[0];
            if (nname.StartsWith("Fac"))
            {
                string dbtype = "Ins" + nname.Substring(3);
                if (conn.content == "Get")
                {
                    return "ModelGet" + dbtype + "(Predicate<" + dbtype + "> sel, Action<List<" + dbtype + ">> ret)";
                }
                if (conn.content == "Crt")
                {
                    return "ModelCrt" + dbtype + "(" + dbtype + " val)";
                }
                if (conn.content == "Del")
                {
                    return "ModelDel" + dbtype + "(Predicate<" + dbtype + "> sel)";
                }
            }
            if (nname.StartsWith("DS"))
            {
                string dbtype = nname.Substring(2);
                if (conn.content == "Get")
                {
                    return "ModelGet" + dbtype + "(Action<" + dbtype + "> ret)";
                }
                if (conn.content == "Set")
                {
                    return "ModelSet" + dbtype + "(" + dbtype + " val)";
                }
            }
            if (nname.StartsWith("DB"))
            {
                string dbtype = nname.Substring(2);
                if (conn.content == "Get")
                {
                    return "ModelGet" + dbtype + "(Predicate<" + dbtype + "> sel, Action<List<" + dbtype + ">> ret)";
                }
                if (conn.content == "Set")
                {
                    return "ModelSet" + dbtype + "(List<" + dbtype + "> vals)";
                }
                if (conn.content == "Mod")
                {
                    return "ModelMod" + dbtype + "(Predicate<" + dbtype + "> sel, Action<" + dbtype + "> mod)";
                }
                if (conn.content == "Del")
                {
                    return "ModelDel" + dbtype + "(Predicate<" + dbtype + "> sel)";
                }
                if (conn.content == "Crt")
                {
                    return "ModelCrt" + dbtype + "(" + dbtype + " val)";
                }
                if (conn.content == "Fnd")
                {
                    return "ModelFnd" + dbtype + "(Action<List<" + dbtype + ">> ret)";
                }
            }
        }

        // Auto flow
        IElementModel start = Select(conn.connInf.start);
        string content = "";
        if (conn.content == "" && start is NodeModel)
        {
            NodeModel node = (NodeModel)start;
            foreach (IElementModel element in elements)
            {
                if (element is ConnModel)
                {
                    ConnModel cm = (ConnModel)element;
                    if (cm.connInf.end == node.GetSelectInf())
                    {
                        string pluscontent = GetConnContent(cm);
                        if (pluscontent.Length > 0)
                        {
                            content += pluscontent;
                            content += ", ";
                            // TODO If source identical, then broken
                        }
                    }
                }
            }
            content = content.Substring(0, content.Length - 2);
            return content;
        }
        return conn.content;
    }

    void FullParseBracket(ref string stype, string content, string nodename)
    {
        if (content.EndsWith(")"))
        {
            int st = content.IndexOf("(");
            stype = content.Substring(0, st);
            string scontwithend = content.Substring(st + 1);
            string scont = scontwithend.Substring(0, scontwithend.Length - 1);
            ParseBracket(stype, scont);
        }
        else
        {
            stype = "Model" + nodename;
            ParseBracket(stype, content);
        }
    }

    void ParseBracket(string name, string content)
    {
        if (content.ToUpper() == content)
        {
            ProgramEnum e = new ProgramEnum(name);
            string[] cs = content.Split(',');
            foreach (string c in cs)
            {
                e.AddEnum(c.Trim());
            }
            enums.Add(e);
        }
        else
        {
            ProgramStruct s = new ProgramStruct(name);
            int level = 0;
            int bra1start = 0;
            int bra1end = 0;
            int bra1name = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == '(')
                {
                    if (level == 0)
                    {
                        bra1start = i;
                    }
                    level++;
                }
                if (name[i] == ')')
                {
                    level--;
                    if (level == 0)
                    {
                        bra1end = i;
                        for (int j = i; j > -1; j--)
                        {
                            if (name[j] == ' ')
                            {
                                bra1name = j + 1;
                            }
                            if (j == 0)
                            {
                                bra1name = 0;
                            }
                        }
                        ParseBracket(name.Substring(bra1name, bra1start - bra1name), name.Substring(bra1start + 1, bra1end - bra1start - 1));
                        name = name.Substring(0, bra1start) + name.Substring(bra1end + 1);
                    }
                }
            }
            string[] pairs = name.Split(',');
            for (int i = 0; i < pairs.Length; i++)
            {
                string pair = pairs[i];
                if (pair[0] == ' ')
                {
                    pair = pair.Substring(1);
                }
                string[] split = pair.Split(' ');
                if (split.Length == 1)
                {
                    string t = split[0];
                    if (t != "null")
                    {
                        if (t.EndsWith("[]"))
                        {
                            s.AddPair(t, t.Substring(0, t.Length - 2).ToLower() + "s");
                        }
                        else
                        {
                            s.AddPair(t, t.ToLower());
                        }
                    }
                }
                else
                {
                    s.AddPair(split[0], split[1]);
                }
            }
            structs.Add(s);
        }
    }

    IElementModel Select(SelectInf si)
    {
        foreach (IElementModel model in elements)
        {
            if (model.GetSelectInf() == si)
            {
                return model;
            }
        }
        return null;
    }
    ProgramClass GetClass(NodeModel node)
    {
        foreach (ProgramClass c in classes)
        {
            if (c.insName == NodeToName(node))
            {
                return c;
            }
        }
        return null;
    }
    string NodeToName(NodeModel node)
    {
        string[] split = node.name.Split(' ');
        if (split.Length == 1)
        {
            return Util.ToSmallCamel(node.name);
        }
        else
        {
            return Util.ToSmallCamel(split[0]) + split[1];
        }
    }

    bool ContainInDefinedStructs(string n)
    {
        foreach (string s in definedStructs)
        {
            if (n == s)
            {
                return true;
            }
        }
        return false;
    }

    void RevertDefinedStructs()
    {
        definedStructs = new List<string>()
        {
            "int",
            "Vector3",
            "string",
            "float",
            "double",
            "char",
            "bool",
            "Vector2"
        };
    }
}

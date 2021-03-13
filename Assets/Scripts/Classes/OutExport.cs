using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class OutExport : MonoBehaviour, IProExportToOutExportReceiver
{
    public InputField projectDir;
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
        if (projectDir.text?.Length > 0)
        {
            ProgramSaver.scriptsPath = projectDir.text;
        }
        int buildCount = fei.buildCount;

        // Init classes
        classes.Clear();
        structs.Clear();
        enums.Clear();
        ProgramMainClass mainClass = new ProgramMainClass();
        ProgramEditorClass editorClass = new ProgramEditorClass();

        List<string> insNames = new List<string>();
        foreach (IElementModel element in elements)
        {
            if (element is NodeModel)
            {
                NodeModel node = (NodeModel)element;
                string className = NodeToClassName(node);
                string insName = NodeToName(node);

                if (!insNames.Contains(insName))
                {
                    bool isDB = className.StartsWith("DB") || className.StartsWith("DS") || className.StartsWith("Fac");
                    TypeDB typeDB = TypeDB.DB;
                    if (className.StartsWith("DS"))
                    {
                        typeDB = TypeDB.DS;
                    }
                    if (className.StartsWith("Fac"))
                    {
                        typeDB = TypeDB.FC;
                    }

                    ProgramClass c = null;
                    if (className.StartsWith("Ins"))
                    {
                        c = new ProgramInstance(className);
                    }
                    else if (isDB)
                    {
                        c = new ProgramClassDB(className, typeDB);
                    }
                    else
                    {
                        c = new ProgramClass(className);
                    }
                    if (insName != Util.ToSmallCamel(className))
                    {
                        c.OverrideName(insName);
                    }
                    classes.Add(c);
                    insNames.Add(insName);

                    if (className.StartsWith("DB") || className.StartsWith("DS"))
                    {
                        ProgramStruct ps = new ProgramStruct(className.Substring(2));
                        structs.Add(ps);
                    }
                }
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
                foreach (IElementModel e in elements)
                {
                    if (e is ConnModel)
                    {
                        ConnModel conn = (ConnModel)e;
                        if (conn.connInf.start == element.GetSelectInf())
                        {
                            outs.Add(conn);
                        }
                        if (conn.connInf.end == element.GetSelectInf())
                        {
                            ins.Add(conn);
                        }
                    }
                }

                // Get type and name
                string stype = "";
                string sname = "";
                List<ConnModel> all = new List<ConnModel>();
                foreach (ConnModel c in outs)
                {
                    all.Add(c);
                }
                foreach (ConnModel c in ins)
                {
                    all.Add(c);
                }
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
                        sname = Util.ToSmallCamel(stype);
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
                    string aname = NodeToClassName(nodea);
                    string bname = NodeToClassName(nodeb);
                    FullParseBracket(ref stype, content, nodea.name, nodeb.name);
                    sname = Util.ToSmallCamel(stype);

                    ProgramInterface sender = new ProgramInterface(false, stype, sname);
                    ProgramInterface receiver = nodeb.name.StartsWith("Ins") ?
                                                new ProgramInterfaceIns(stype, sname) :
                                                new ProgramInterface(true, stype, sname);
                    if (receiver is ProgramInterfaceIns)
                    {
                        ProgramSaver.SaveInterface("/Interfaces",
                                                   (ProgramInterfaceIns)receiver);
                    }
                    foreach (ProgramClass pc in classes)
                    {
                        if (pc.className == aname)
                        {
                            pc.AddInterface(sender);
                        }
                        if (pc.className == bname)
                        {
                            pc.AddInterface(receiver);
                        }
                    }
                    bool connected = false;
                    if (aname.StartsWith("Ins"))
                    {
                        foreach (ProgramClass c in classes)
                        {
                            if (c is ProgramClassDB)
                            {
                                ProgramClassDB db = (ProgramClassDB)c;
                                if (db.cname == aname)
                                {
                                    db.AddConnection(sender, receiver, GetClass(nodea), GetClass(nodeb));
                                    connected = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (bname.StartsWith("Ins"))
                    {
                        connected = true;
                    }
                    if (!connected)
                    {
                        mainClass.AddConnection(sender, receiver, GetClass(nodea), GetClass(nodeb));
                    }
                }
            }
        }

        // Save program
        foreach (ProgramStruct v in structs)
        {
            if (!ContainInDefinedStructs(v.structName))
            {
                ProgramSaver.SaveStruct("/Structs", v);
                definedStructs.Add(v.structName);
            }
        }
        foreach (ProgramEnum e in enums)
        {
            ProgramSaver.SaveEnum("/Enums", e);
        }
        List<string> savedClasses = new List<string>();
        for (int i = 0; i < classes.Count; i++)
        {
            ProgramClass pc = classes[i];
            if (!savedClasses.Contains(pc.className))
            {
                ProgramSaver.SaveClass("/Classes", pc);
                if (pc.NeedMBInstantiate())
                {
                    editorClass.AddClass(pc);
                }
                savedClasses.Add(pc.className);
            }
        }
        ProgramSaver.SaveMainClass("/Main", mainClass);
        ProgramSaver.SaveEditorClass("/Others", editorClass);
        ProgramSaver.SaveUpdater("/Classes");
        ProgramSaver.SaveIMB("/Interfaces");

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
                    return "ModelCrt" + dbtype + "(" + dbtype + " val, Action<" + dbtype + "> ret)";
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

    void FullParseBracket(ref string stype, string content, string nna, string nnb)
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
            stype = "Model" + nna + "To" + nnb;
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
            foreach (ProgramStruct ps in structs)
            {
                if (ps.structName == name)
                {
                    s = ps;
                    break;
                }
            }
            int level = 0;
            int bra1start = 0;
            int bra1end = 0;
            int bra1name = 0;
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == '(')
                {
                    if (level == 0)
                    {
                        bra1start = i;
                    }
                    level++;
                }
                if (content[i] == ')')
                {
                    level--;
                    if (level == 0)
                    {
                        bra1end = i;
                        for (int j = bra1start; j > -1; j--)
                        {
                            if (content[j] == ' ')
                            {
                                bra1name = j + 1;
                                break;
                            }
                            if (j == 0)
                            {
                                bra1name = 0;
                            }
                        }
                        ParseBracket(content.Substring(bra1name, bra1start - bra1name), content.Substring(bra1start + 1, bra1end - bra1start - 1));
                        content = content.Substring(0, bra1start) + content.Substring(bra1end + 1);
                    }
                }
            }
            string[] pairs = content.Split(',');
            for (int i = 0; i < pairs.Length; i++)
            {
                string pair = pairs[i].Trim();
                string[] split = pair.Split(' ');
                if (split.Length == 1)
                {
                    string t = split[0];
                    if (t != "null")
                    {
                        if (t.EndsWith("[]"))
                        {
                            s.AddPair(t, Util.ToSmallCamel(t.Substring(0, t.Length - 2)) + "s");
                        }
                        else
                        {
                            s.AddPair(t, Util.ToSmallCamel(t));
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

    string NodeToClassName(NodeModel node)
    {
        string[] split = node.name.Split(' ');
        if (split.Length == 1)
        {
            return node.name;
        }
        else
        {
            return split[0];
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

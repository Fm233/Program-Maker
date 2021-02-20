using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ProLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Load(Project p)
    {
        get(new DBGetRequest((SelectInf inf) =>
        {
            return true;
        }, (DBGetResult result) =>
        {
            foreach (IElementModel element in result.elements)
            {
                if (!(element is ConnModel))
                {
                    del(new DeleteInf(element.GetSelectInf()));
                }
            }
        }));
        if (p.nodes?.Length > 0)
        {
            foreach (NodeModel node in p.nodes)
            {
                crtNode(node.nodeInf);
                rename(new RenameInf(node.nodeInf.si, node.name));
                SelectInf.AddSelectInf(node.nodeInf.si);
            }
            foreach (InterModel inter in p.inters)
            {
                crtInter(inter.interInf);
                SelectInf.AddSelectInf(inter.interInf.si);
            }
            foreach (ConnModel conn in p.conns)
            {
                crtConn(conn.connInf);
                rename(new RenameInf(conn.connInf.self, conn.content));
                SelectInf.AddSelectInf(conn.connInf.self);
            }
            load(new LoadInf(p.canvasPos, p.builds));
        }
    }

    public Action<DBGetRequest> get { private get; set; }
    public Action<DeleteInf> del { private get; set; }
    public Action<NodeInf> crtNode { private get; set; }
    public Action<ConnInf> crtConn { private get; set; }
    public Action<InterInf> crtInter { private get; set; }
    public Action<RenameInf> rename { private get; set; }
    public Action<LoadInf> load { private get; set; }
}

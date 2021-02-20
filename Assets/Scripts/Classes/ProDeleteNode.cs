using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ProDeleteNode : MonoBehaviour, IExPanelToProDeleteNodeReceiver, IProDeleteNodeToDBDelSender, IDBGetter, IProDeleteNodeToOutDeleteSender
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<DeleteInf> GetIdiReceiver()
    {
        return ReceiveIdi;
    }
    public void ReceiveIdi(DeleteInf idi)
    {
        if (idi.target.type != ElementType.NONE)
        {
            inDbdrAction(new DBDelRequest((IElementModel element) =>
            {
                if (idi.target == element.GetSelectInf())
                {
                    fdiAction(idi);
                    return true;
                }
                if (element is ConnModel)
                {
                    ConnModel conn = (ConnModel)element;
                    if (conn.connInf.start == idi.target)
                    {
                        fdiAction(new DeleteInf(conn.connInf.self));
                        return true;
                    }
                    if (conn.connInf.end == idi.target)
                    {
                        fdiAction(new DeleteInf(conn.connInf.self));
                        return true;
                    }
                }
                return false;
            }));
        }
    }
    Action<DBDelRequest> inDbdrAction;
    public void AddInDbdrReceiver(Action<DBDelRequest> action)
    {
        inDbdrAction += action;
    }
    Action<DBGetRequest> inDbgrAction;
    public void AddInDbgrReceiver(Action<DBGetRequest> action)
    {
        inDbgrAction += action;
    }
    Action<DeleteInf> fdiAction;
    public void AddFdiReceiver(Action<DeleteInf> action)
    {
        fdiAction += action;
    }
}

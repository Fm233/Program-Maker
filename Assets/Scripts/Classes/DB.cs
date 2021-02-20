using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class DB : MonoBehaviour, IDBGetToDBReceiver, IDBSetToDBReceiver, IDBDelToDBReceiver, IDBCrtToDBReceiver, IDBToDBResSender
{
    List<IElementModel> elements = new List<IElementModel>();
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<DBGetRequest> GetOutDbgrReceiver()
    {
        return ReceiveOutDbgr;
    }
    public void ReceiveOutDbgr(DBGetRequest outDbgr)
    {
        List<IElementModel> selected = new List<IElementModel>();
        foreach (IElementModel element in elements)
        {
            if (outDbgr.selector(element.GetSelectInf()))
            {
                selected.Add(element);
            }
        }
        DBGetResult result = new DBGetResult(selected);
        outDbgr.returnAction(result);
    }
    public Action<DBSetRequest> GetOutDbsrReceiver()
    {
        return ReceiveOutDbsr;
    }
    public void ReceiveOutDbsr(DBSetRequest outDbsr)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            IElementModel element = elements[i];
            if (outDbsr.selector(element.GetSelectInf()))
            {
                outDbsr.setAction(elements[i]);
            }
        }
    }
    public Action<DBDelRequest> GetOutDbdrReceiver()
    {
        return ReceiveOutDbdr;
    }
    public void ReceiveOutDbdr(DBDelRequest outDbdr)
    {
        List<IElementModel> del = new List<IElementModel>();
        for (int i = 0; i < elements.Count; i++)
        {
            if (outDbdr.selector(elements[i]))
            {
                del.Add(elements[i]);
            }
        }
        foreach (IElementModel element in del)
        {
            elements.Remove(element);
        }
    }
    public Action<DBCreateRequest> GetOutDbcrReceiver()
    {
        return ReceiveOutDbcr;
    }
    public void ReceiveOutDbcr(DBCreateRequest outDbcr)
    {
        elements.Add(outDbcr.element);
    }
    Action<DBGetResult> inDbgrAction;
    public void AddInDbgrReceiver(Action<DBGetResult> action)
    {
        inDbgrAction += action;
    }
}

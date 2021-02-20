
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ExPanel : MonoBehaviour, IInDCToExPanelReceiver, IDBGetter, IInRCToExPanelReceiver, IInSelectToExPanelReceiver, IExPanelToProCreateNodeSender, IInMouseToExPanelReceiver, IExPanelToProTryConnSender, IExPanelToProCreateConnSender, IInDeleteToExPanelReceiver, IExPanelToProDeleteNodeSender, IExPanelToProCreateInterSender
{
    public Transform canvas;
    SelectInf selection;
    bool isTryingConn = false;
    SelectInf tryConnFirst;
    void Start()
    {
        selection = SelectInf.none;
    }
    public Action<Vector2> GetDcPosReceiver()
    {
        return ReceiveDcPos;
    }
    public void ReceiveDcPos(Vector2 indcPos)
    {
        Vector2 dcPos = indcPos - (Vector2)canvas.position;
        if (selection.type == ElementType.NONE)
        {
            iniAction(new NodeInf(SelectInf.Create(ElementType.NODE), dcPos));
        }
        if (selection.type == ElementType.CONN)
        {
            SelectInf inter = SelectInf.Create(ElementType.INTER);
            iiiAction(new InterInf(inter, dcPos));
            inDbgrAction(new DBGetRequest((SelectInf inf) =>
            {
                return inf == selection;
            }, (DBGetResult result) =>
            {
                IElementModel element = result.elements[0];
                if (element is ConnModel)
                {
                    ConnModel conn = (ConnModel)element;
                    iciAction(new ConnInf(SelectInf.Create(ElementType.CONN), conn.connInf.start, inter));
                    iciAction(new ConnInf(SelectInf.Create(ElementType.CONN), inter, conn.connInf.end));
                }
            }));
            idiAction(new DeleteInf(selection));
        }
    }
    public Action<Vector2> GetRcPosReceiver()
    {
        return ReceiveRcPos;
    }
    public void ReceiveRcPos(Vector2 inrcPos)
    {
        Vector2 rcPos = inrcPos - (Vector2)canvas.position;
        if (selection.type == ElementType.NODE || selection.type == ElementType.INTER)
        {
            if (!isTryingConn)
            {
                isTryingConn = true;
                tryConnFirst = selection;
                tciAction(new TryConnInf(TryConnInfType.START, rcPos));
            }
            else
            {
                isTryingConn = false;
                tciAction(TryConnInf.end);
                iciAction(new ConnInf(SelectInf.Create(ElementType.CONN), tryConnFirst, selection));
            }
        }
        else
        {
            if (isTryingConn)
            {
                isTryingConn = false;
                tciAction(TryConnInf.end);
            }
        }
    }
    public Action<SelectInf> GetSiReceiver()
    {
        return ReceiveSi;
    }
    public void ReceiveSi(SelectInf si)
    {
        selection = si;
    }
    Action<NodeInf> iniAction;
    public void AddIniReceiver(Action<NodeInf> action)
    {
        iniAction += action;
    }
    public Action<Vector2> GetMousePosReceiver()
    {
        return ReceiveMousePos;
    }
    public void ReceiveMousePos(Vector2 inmousePos)
    {
        Vector2 mousePos = inmousePos - (Vector2)canvas.position;
        if (isTryingConn)
        {
            tciAction(new TryConnInf(TryConnInfType.POS, mousePos));
        }
    }
    Action<TryConnInf> tciAction;
    public void AddTciReceiver(Action<TryConnInf> action)
    {
        tciAction += action;
    }
    Action<ConnInf> iciAction;
    public void AddIciReceiver(Action<ConnInf> action)
    {
        iciAction += action;
    }
    public Action<Vector2> GetDeletePosReceiver()
    {
        return ReceiveDeletePos;
    }
    public void ReceiveDeletePos(Vector2 indeletePos)
    {
        Vector2 deletePos = indeletePos - (Vector2)canvas.position;
        idiAction(new DeleteInf(selection));
    }
    Action<DeleteInf> idiAction;
    public void AddIdiReceiver(Action<DeleteInf> action)
    {
        idiAction += action;
    }
    Action<InterInf> iiiAction;
    public void AddIiiReceiver(Action<InterInf> action)
    {
        iiiAction += action;
    }
    Action<DBGetRequest> inDbgrAction;
    public void AddInDbgrReceiver(Action<DBGetRequest> action)
    {
        inDbgrAction += action;
    }
}

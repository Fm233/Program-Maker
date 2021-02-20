using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Node : MonoBehaviour, IInstanceRenameable
{
    void Start()
    {
        nmiAction = GameObject.Find("InMove Instance").GetComponent<InMove>().GetMiReceiver();
        nsiAction = GameObject.Find("InSelect Instance").GetComponent<InSelect>().GetSiReceiver();
        nriAction = GameObject.Find("InRename Instance").GetComponent<InRename>().GetRiReceiver();
        input.ActivateInputField();
    }
    void Update()
    {
        // Update here
    }
    public InputField input;
    public void OnNameChange()
    {
        nriAction(new RenameInf(me, input.text));
    }
    Vector3 dragDelta;
    private void OnMouseDown()
    {
        dragDelta = transform.localPosition - Input.mousePosition;
    }
    private void OnMouseDrag()
    {
        nmiAction(new MoveInf(me, Input.mousePosition + dragDelta));
    }
    SelectInf me;
    public void SetSelectInf(SelectInf inf)
    {
        me = inf;
    }
    private void OnMouseOver()
    {
        nsiAction(me);
    }
    private void OnMouseExit()
    {
        nsiAction(SelectInf.none);
    }
    private void OnDisable()
    {
        nsiAction?.Invoke(SelectInf.none);
    }

    public void Rename(string name)
    {
        input.text = name;
    }

    Action<SelectInf> nsiAction;
    Action<MoveInf> nmiAction;
    Action<RenameInf> nriAction;
}

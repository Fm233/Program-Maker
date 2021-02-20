using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Conn : MonoBehaviour, IInstanceRenameable
{
    public LineRenderer lineRenderer;
    public InputField input;
    GameObject from;
    GameObject to;
    void Start()
    {
        nsiAction = GameObject.Find("InSelect Instance").GetComponent<InSelect>().GetSiReceiver();
        nriAction = GameObject.Find("InRename Instance").GetComponent<InRename>().GetRiReceiver();
        input.ActivateInputField();
    }
    void Update()
    {
        lineRenderer.SetPosition(0, from.transform.position);
        lineRenderer.SetPosition(1, to.transform.position);
        transform.position = (from.transform.position + to.transform.position) / 2;
    }
    public void OnNameChange()
    {
        nriAction(new RenameInf(me, input.text));
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
    public void SetAttached(GameObject f, GameObject t)
    {
        from = f;
        to = t;
    }

    public void Rename(string name)
    {
        input.text = name;
    }

    Action<SelectInf> nsiAction;
    Action<RenameInf> nriAction;
}

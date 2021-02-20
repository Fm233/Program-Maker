using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        nmiAction = GameObject.Find("InMove Instance").GetComponent<InMove>().GetMiReceiver();
        nsiAction = GameObject.Find("InSelect Instance").GetComponent<InSelect>().GetSiReceiver();
    }

    // Update is called once per frame
    void Update()
    {

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
    Action<SelectInf> nsiAction;
    Action<MoveInf> nmiAction;
}

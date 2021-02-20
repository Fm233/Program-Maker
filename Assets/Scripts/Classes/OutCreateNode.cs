using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class OutCreateNode : MonoBehaviour, IProCreateNodeToOutCreateNodeReceiver
{
    public GameObject nodePrefab;
    public Transform canvas;
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<NodeInf> GetFniReceiver()
    {
        return ReceiveFni;
    }
    public void ReceiveFni(NodeInf fni)
    {
        GameObject node = Instantiate(nodePrefab);
        node.transform.parent = canvas;
        node.transform.localPosition = fni.pos;
        node.name = fni.si.ToString();
        node.GetComponent<Node>().SetSelectInf(fni.si);
    }
}

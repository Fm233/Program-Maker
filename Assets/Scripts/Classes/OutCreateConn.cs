using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class OutCreateConn : MonoBehaviour, IProCreateConnToOutCreateConnReceiver
{
    public GameObject connPrefab;
    public Transform canvas;
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<ConnInf> GetFciReceiver()
    {
        return ReceiveFci;
    }
    public void ReceiveFci(ConnInf fci)
    {
        GameObject conn = Instantiate(connPrefab);
        conn.transform.parent = canvas;
        conn.name = fci.self.ToString();
        Conn comp = conn.GetComponentInChildren<Conn>();
        comp.SetAttached(GameObject.Find(fci.start.ToString()), GameObject.Find(fci.end.ToString()));
        comp.SetSelectInf(fci.self);
    }
}

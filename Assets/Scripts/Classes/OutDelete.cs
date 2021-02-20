using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class OutDelete : MonoBehaviour, IProDeleteNodeToOutDeleteReceiver
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<DeleteInf> GetFdiReceiver()
    {
        return ReceiveFdi;
    }
    public void ReceiveFdi(DeleteInf fdi)
    {
        GameObject.Find(fdi.target.ToString())?.SetActive(false);
    }
}

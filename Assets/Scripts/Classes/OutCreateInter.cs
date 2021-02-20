using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class OutCreateInter : MonoBehaviour, IProCreateInterToOutCreateInterReceiver
{
    public GameObject interPrefab;
    public Transform canvas;
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<InterInf> GetFiiReceiver()
    {
        return ReceiveFii;
    }
    public void ReceiveFii(InterInf fii)
    {
        GameObject inter = Instantiate(interPrefab);
        inter.transform.parent = canvas;
        inter.transform.localPosition = fii.pos;
        inter.name = fii.si.ToString();
        inter.GetComponent<Inter>().SetSelectInf(fii.si);
    }
}

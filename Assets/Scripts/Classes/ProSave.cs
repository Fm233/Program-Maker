using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Save(SaveInf saveInf)
    {
        get(new DBGetRequest((SelectInf inf) =>
        {
            return true;
        }, (DBGetResult result) =>
        {
            save(new FinalSaveInf(result.elements, saveInf.name, saveInf.canvasPos, saveInf.buildIndex));
        }));
    }
    public Action<DBGetRequest> get { private get; set; }
    public Action<FinalSaveInf> save { private get; set; }
}

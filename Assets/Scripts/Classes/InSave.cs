using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Stopwatch = System.Diagnostics.Stopwatch;

public class InSave : MonoBehaviour
{
    const double SAVE_TIME = 1;
    public InputField pname;
    public InputField builds;
    public InputField pdir;
    public Transform canvas;
    //Stopwatch saveTimer = new Stopwatch();

    void Start()
    {
        //saveTimer.Restart();
    }
    void Update()
    {
        /*if (saveTimer.Elapsed.TotalSeconds > SAVE_TIME)
        {
            saveTimer.Restart();
            Save();
        }*/
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        JsonLoader.Save("", "config", new Config(pname.text, pdir.text)); // NC
        save(new SaveInf(pname.text, canvas.transform.position, int.Parse(builds.text)));
    }

    public Action<SaveInf> save { private get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class InLoad : MonoBehaviour
{
    public InputField pname;
    public InputField pdir;
    private void Start()
    {
        Config config = JsonLoader.Load<Config>("", "config"); // NC
        pname.text = config.projectName;
        pdir.text = config.projectDir;
        Load();
    }
    public void Load()
    {
        Project p = JsonLoader.Load<Project>("Projects", pname.text);
        load(p);
    }

    public Action<Project> load { private get; set; }
}

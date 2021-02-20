using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Config
{
    public string projectName;

    public Config(string projectName)
    {
        this.projectName = projectName;
    }
}
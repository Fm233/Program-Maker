using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Config
{
    public string projectName;
    public string projectDir;

    public Config(string projectName, string projectDir)
    {
        this.projectName = projectName;
        this.projectDir = projectDir;
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Process = System.Diagnostics.Process;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    public InputField inputExport;

    public void Folder()
    {
        int.TryParse(inputExport.text, out int buildCount);
        string path = Application.persistentDataPath + "/Build_" + (buildCount - 1).ToString();
        OpenDirectory(path);
    }

    public static void OpenDirectory(string path)
    {
        // 新开线程防止锁死
        Thread newThread = new Thread(new ParameterizedThreadStart(CmdOpenDirectory));
        newThread.Start(path);
    }

    private static void CmdOpenDirectory(object obj)
    {
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.Arguments = "/c start " + obj.ToString();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();

        p.WaitForExit();
        p.Close();
    }
}

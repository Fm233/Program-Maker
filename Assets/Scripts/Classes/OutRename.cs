using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class OutRename : MonoBehaviour, IProRenameToOutRenameReceiver
{
    void Start()
    {
        // Start here
    }
    void Update()
    {
        // Update here
    }
    public Action<RenameInf> GetFriReceiver()
    {
        return ReceiveFri;
    }
    public void ReceiveFri(RenameInf fri)
    {
        if (GameObject.Find(fri.si.ToString()).TryGetComponent(out IInstanceRenameable renameable))
        {
            renameable.Rename(fri.name);
        }
    }
}

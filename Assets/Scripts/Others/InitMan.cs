#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
public class InitMan : Editor
{
    [MenuItem("InitMan/Init")]
    public static void Init()
    {
        GameObject mainObject = new GameObject();
        mainObject.name = "Main";
        Main main = mainObject.AddComponent<Main>();
        GameObject inDC = new GameObject();
        inDC.name = "InDC Instance";
        main.inDC = inDC.AddComponent<InDC>();
        GameObject inRC = new GameObject();
        inRC.name = "InRC Instance";
        main.inRC = inRC.AddComponent<InRC>();
        GameObject inSelect = new GameObject();
        inSelect.name = "InSelect Instance";
        main.inSelect = inSelect.AddComponent<InSelect>();
        GameObject exPanel = new GameObject();
        exPanel.name = "ExPanel Instance";
        main.exPanel = exPanel.AddComponent<ExPanel>();
        GameObject proCreateNode = new GameObject();
        proCreateNode.name = "ProCreateNode Instance";
        main.proCreateNode = proCreateNode.AddComponent<ProCreateNode>();
        GameObject proCreateConn = new GameObject();
        proCreateConn.name = "ProCreateConn Instance";
        main.proCreateConn = proCreateConn.AddComponent<ProCreateConn>();
        GameObject proTryConn = new GameObject();
        proTryConn.name = "ProTryConn Instance";
        main.proTryConn = proTryConn.AddComponent<ProTryConn>();
        GameObject inMouse = new GameObject();
        inMouse.name = "InMouse Instance";
        main.inMouse = inMouse.AddComponent<InMouse>();
        GameObject inDelete = new GameObject();
        inDelete.name = "InDelete Instance";
        main.inDelete = inDelete.AddComponent<InDelete>();
        GameObject proDeleteNode = new GameObject();
        proDeleteNode.name = "ProDeleteNode Instance";
        main.proDeleteNode = proDeleteNode.AddComponent<ProDeleteNode>();
        GameObject proRename = new GameObject();
        proRename.name = "ProRename Instance";
        main.proRename = proRename.AddComponent<ProRename>();
        GameObject proMove = new GameObject();
        proMove.name = "ProMove Instance";
        main.proMove = proMove.AddComponent<ProMove>();
        GameObject proExport = new GameObject();
        proExport.name = "ProExport Instance";
        main.proExport = proExport.AddComponent<ProExport>();
        GameObject dBGet = new GameObject();
        dBGet.name = "DBGet Instance";
        main.dBGet = dBGet.AddComponent<DBGet>();
        GameObject dB = new GameObject();
        dB.name = "DB Instance";
        main.dB = dB.AddComponent<DB>();
        GameObject outRename = new GameObject();
        outRename.name = "OutRename Instance";
        main.outRename = outRename.AddComponent<OutRename>();
        GameObject outMove = new GameObject();
        outMove.name = "OutMove Instance";
        main.outMove = outMove.AddComponent<OutMove>();
        GameObject outDelete = new GameObject();
        outDelete.name = "OutDelete Instance";
        main.outDelete = outDelete.AddComponent<OutDelete>();
        GameObject dBSet = new GameObject();
        dBSet.name = "DBSet Instance";
        main.dBSet = dBSet.AddComponent<DBSet>();
        GameObject dBDel = new GameObject();
        dBDel.name = "DBDel Instance";
        main.dBDel = dBDel.AddComponent<DBDel>();
        GameObject outExport = new GameObject();
        outExport.name = "OutExport Instance";
        main.outExport = outExport.AddComponent<OutExport>();
        GameObject dBCrt = new GameObject();
        dBCrt.name = "DBCrt Instance";
        main.dBCrt = dBCrt.AddComponent<DBCrt>();
        GameObject outCreateNode = new GameObject();
        outCreateNode.name = "OutCreateNode Instance";
        main.outCreateNode = outCreateNode.AddComponent<OutCreateNode>();
        GameObject outCreateConn = new GameObject();
        outCreateConn.name = "OutCreateConn Instance";
        main.outCreateConn = outCreateConn.AddComponent<OutCreateConn>();
        GameObject inRename = new GameObject();
        inRename.name = "InRename Instance";
        main.inRename = inRename.AddComponent<InRename>();
        GameObject inMove = new GameObject();
        inMove.name = "InMove Instance";
        main.inMove = inMove.AddComponent<InMove>();
        GameObject inExport = new GameObject();
        inExport.name = "InExport Instance";
        main.inExport = inExport.AddComponent<InExport>();
        GameObject dBRes = new GameObject();
        dBRes.name = "DBRes Instance";
        main.dBRes = dBRes.AddComponent<DBRes>();
        GameObject proCreateInter = new GameObject();
        proCreateInter.name = "ProCreateInter Instance";
        main.proCreateInter = proCreateInter.AddComponent<ProCreateInter>();
        GameObject outCreateInter = new GameObject();
        outCreateInter.name = "OutCreateInter Instance";
        main.outCreateInter = outCreateInter.AddComponent<OutCreateInter>();
    }
}
#endif

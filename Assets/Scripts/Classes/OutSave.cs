using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSave : MonoBehaviour
{
    public void Save(FinalSaveInf saveInf)
    {
        List<NodeModel> nodes = new List<NodeModel>();
        List<ConnModel> conns = new List<ConnModel>();
        List<InterModel> inters = new List<InterModel>();
        foreach (IElementModel element in saveInf.models)
        {
            if (element is NodeModel)
            {
                nodes.Add((NodeModel)element);
            }
            if (element is ConnModel)
            {
                conns.Add((ConnModel)element);
            }
            if (element is InterModel)
            {
                inters.Add((InterModel)element);
            }
        }
        Project p = new Project(nodes.ToArray(), conns.ToArray(), inters.ToArray(), saveInf.canvasPos, saveInf.buildIndex);
        JsonLoader.Save("Projects", saveInf.name, p);
    }
}

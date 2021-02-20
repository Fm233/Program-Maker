using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutLoad : MonoBehaviour
{
    public Transform canvas;
    public InputField bc;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Load(LoadInf li)
    {
        bc.text = li.buildCount.ToString();
        canvas.transform.position = li.canvasPos;
    }
}

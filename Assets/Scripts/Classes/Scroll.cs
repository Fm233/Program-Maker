using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public Transform canvas;
    Vector3 canvasDragDelta;
    float lastDelta = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            canvasDragDelta = canvas.transform.position - Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            canvas.transform.position = canvasDragDelta + Input.mousePosition;
        }
        lastDelta = Input.GetAxis("Mouse ScrollWheel") * 200f + lastDelta * 0.8f;
        canvas.position += Vector3.down * lastDelta;
    }
}

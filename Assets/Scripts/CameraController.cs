using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        cam.orthographicSize = Screen.height / 2;
        cam.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, -20);
    }
}

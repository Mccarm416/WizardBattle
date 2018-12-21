using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelDensityCamera : MonoBehaviour
{
    public float pixelsToUnits = 1;
    public Camera camera;

    void Update()
    {
        camera.orthographicSize = Screen.height / pixelsToUnits / 2;

    }
}

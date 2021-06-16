using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HowSize { ZoomIn, Default, ZoomOut }

public class CameraZoom : MonoBehaviour
{
    public static CameraZoom _cameraZoom;

    private Camera camera;
    private HowSize howSize = HowSize.Default;
    [SerializeField]
    private float zoomSpd = 0.1f;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        _cameraZoom = GetComponent<CameraZoom>();
    }

    public void ZoomIn()
    {
        howSize = HowSize.ZoomIn;
    }

    public void Default()
    {
        howSize = HowSize.Default;
    }

    public void ZoomOut()
    {
        howSize = HowSize.ZoomOut;
    }

    private void Update()
    {
        if(howSize == HowSize.Default)
        {
            camera.orthographicSize = 8;
        }
        else if(howSize == HowSize.ZoomIn)
        {

        }
        else
        {

        }
    }
}

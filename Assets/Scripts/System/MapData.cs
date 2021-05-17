using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField]
    private Vector2 size = Vector2.zero;
    public Vector2 Size => size;
    [SerializeField]
    private Vector2 position = Vector2.zero;
    public Vector2 Position => position;
    [SerializeField]
    private bool autoSize = true;
    [SerializeField]
    private bool autoPosition = true;
    [SerializeField]
    private bool autoAssignMapData = true;

    private void Start()
    {
        if (autoSize)
            size = transform.localScale;

        if (autoPosition)
            position = transform.position;

        if (autoAssignMapData)
            LazyCamera.instance.SetupMapData(this);
    }
}

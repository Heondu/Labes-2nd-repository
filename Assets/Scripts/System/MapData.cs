using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField]
    private Vector2 mapSize;
    [SerializeField]
    private bool autoSetMapSize = true;

    private void Start()
    {
        if (autoSetMapSize)
            mapSize = transform.localScale;

        LazyCamera.instance.mapSize = mapSize;
    }
}

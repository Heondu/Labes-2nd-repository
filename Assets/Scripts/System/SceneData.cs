using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData instance;

    public string prevScene;
    public Vector3 prevScenePos { get; set; } = Vector3.zero;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}

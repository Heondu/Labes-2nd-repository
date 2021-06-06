using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData instance;

    public string prevScene;
    public Vector3 prevScenePos { get; set; } = Vector3.zero;
    public MapData mapdata;

    public string mainScene = "MainScene";
    public string guardDungeon = "GuardDungeon";
    public string attackDungeon = "AttackDungeon";
    public string dungeon01 = "Dungeon01";
    public string bossDungeon = "BossDungeon";

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}

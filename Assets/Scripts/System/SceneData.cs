using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData instance;

    public string prevScene;
    public Vector3 prevScenePos { get; set; } = Vector3.zero;
    public MapData mapdata;

    public static string mainScene = "MainScene";
    public static string guardDungeon = "GuardDungeon";
    public static string attackDungeon = "AttackDungeon";
    public static string dungeon01 = "Dungeon01";
    public static string bossDungeon = "BossDungeon";
    public static string loadingScene = "LoadingScene";

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}

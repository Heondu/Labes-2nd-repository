using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DungeonType { Forest, Snow, castle }

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 startingPos = Vector3.zero;
    [SerializeField]
    private DungeonType dungeonType = DungeonType.Forest;

    public Sprite[] background;
    public Sprite[] wall;
    public Sprite[] obstacleTop;
    public Sprite[] obstacleBottom;
    public Sprite[] obstacleLeft;
    public Sprite[] obstacleRight;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        player.transform.position = startingPos;
    }

    public DungeonType GetDungeonType()
    {
        return dungeonType;
    }
}

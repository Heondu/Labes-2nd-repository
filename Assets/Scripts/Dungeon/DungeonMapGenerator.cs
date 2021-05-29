using UnityEngine;

public class DungeonMapGenerator : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer background;
    [SerializeField]
    private SpriteRenderer wall;
    [SerializeField]
    private SpriteRenderer obstacleTop;
    [SerializeField]
    private SpriteRenderer obstacleBottom;
    [SerializeField]
    private SpriteRenderer obstacleLeft;
    [SerializeField]
    private SpriteRenderer obstacleRight;

    private DungeonManager dungeonManager;

    private void Awake()
    {
        dungeonManager = FindObjectOfType<DungeonManager>();
        int index = (int)dungeonManager.GetDungeonType();

        background.sprite = dungeonManager.background[index];
        wall.sprite = dungeonManager.wall[index];

        if (obstacleTop != null) obstacleTop.sprite = dungeonManager.obstacleTop[index];
        if (obstacleBottom != null) obstacleBottom.sprite = dungeonManager.obstacleBottom[index];
        if (obstacleLeft != null) obstacleLeft.sprite = dungeonManager.obstacleLeft[index];
        if (obstacleRight != null) obstacleRight.sprite = dungeonManager.obstacleRight[index];
    }
}
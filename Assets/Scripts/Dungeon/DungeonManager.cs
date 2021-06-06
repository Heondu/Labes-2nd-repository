using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private bool respawnToMainSceneAtDeath = true;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.onDeath.AddListener(OnPlayerDeath);
        player.transform.position = startingPos;
    }

    public DungeonType GetDungeonType()
    {
        return dungeonType;
    }

    private void OnPlayerDeath()
    {
        if (respawnToMainSceneAtDeath == false) return;
        if (SceneManager.GetActiveScene().name == SceneData.instance.attackDungeon) return;
        if (SceneManager.GetActiveScene().name == SceneData.instance.guardDungeon) return;


        player.status.HP = player.status.maxHP;
        player.transform.position = Vector3.zero;
        LoadingSceneManager.LoadScene(SceneData.instance.mainScene);
    }
}

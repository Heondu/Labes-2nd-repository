using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AttackingDungeon : MonoBehaviour
{
    [SerializeField]
    private RegenManager regenManager;
    private Player player;

    [SerializeField]
    private TextMeshProUGUI killText;
    [SerializeField]
    private TextMeshProUGUI waveText;

    [SerializeField]
    private bool loadSceneOnDeath = true;

    private int kill;
    private int wave = 1;

    private int originHP;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneData.attackDungeon));

        regenManager.onRegen.AddListener(IncreaseWave);
        regenManager.onEnemyDeath.AddListener(IncreaseKillNum);
        player.onDeath.AddListener(OnPlayerDeath);

        originHP = player.status.HP;
        player.status.HP = player.status.maxHP;
    }

    private void Update()
    {
        killText.text = kill.ToString();
        waveText.text = wave.ToString();
    }

    public void OnPlayerDeath()
    {
        if (loadSceneOnDeath == false) return;

        player.status.HP = originHP;
        player.transform.position = SceneData.instance.prevScenePos;
        player.SetMapData(SceneData.instance.mapdata);
        LazyCamera.instance.SetMapData(SceneData.instance.mapdata);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneData.instance.prevScene));
        SceneManager.UnloadSceneAsync(SceneData.attackDungeon);
    }

    private void IncreaseWave()
    {
        wave++;
    }

    private void IncreaseKillNum()
    {
        kill++;
    }
}

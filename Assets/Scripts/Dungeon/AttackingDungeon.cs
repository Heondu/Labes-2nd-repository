using UnityEngine;
using TMPro;

public class AttackingDungeon : MonoBehaviour
{
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

    private void Awake()
    {
        regenManager = FindObjectOfType<RegenManager>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        regenManager.onRegen.AddListener(IncreaseWave);
        regenManager.onEnemyDeath.AddListener(IncreaseKillNum);
    }

    private void Update()
    {
        killText.text = kill.ToString();
        waveText.text = wave.ToString();

        if (player.status.HP == 0)
        {
            if (loadSceneOnDeath == false) return;

            player.transform.position = SceneData.instance.prevScenePos;
            LoadingSceneManager.LoadScene(SceneData.instance.prevScene);
        }
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

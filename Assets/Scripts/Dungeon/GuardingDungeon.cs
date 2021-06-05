using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GuardingDungeon : MonoBehaviour
{
    [SerializeField]
    private RegenManager regenManager;
    [SerializeField]
    private Tower tower;
    private Player player;

    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI waveText;

    [SerializeField]
    private bool loadSceneOnDeath = true;

    private float time = 0;
    private int hour = 0;
    private int min = 0;
    private int sec = 0;

    private int wave = 1;

    private int originHP;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneData.instance.guardDungeon));

        regenManager.onRegen.AddListener(IncreaseWave);
        player.onDeath.AddListener(OnPlayerDeath);

        originHP = player.status.HP;
        player.status.HP = player.status.maxHP;
    }

    private void Update()
    {
        time += Time.deltaTime;

        min = (int)time / 60;
        hour = min / 60;
        min = min % 60;
        sec = (int)time % 60;

        if (hour == 0) timeText.text = min.ToString("00") + ":" + sec.ToString("00");
        else timeText.text = hour.ToString("00") + ":" + min.ToString("00") + ":" + sec.ToString("00");

        waveText.text = wave.ToString();

        if (tower.status.HP == 0)
        {
            LoadScene();
        }
    }

    private void OnPlayerDeath()
    {
        if (player.status.HP == 0)
        {
            if (loadSceneOnDeath == false) return;

            LoadScene();
        }
    }

    private void LoadScene()
    {
        player.status.HP = originHP;
        player.transform.position = SceneData.instance.prevScenePos;
        player.SetMapData(SceneData.instance.mapdata);
        LazyCamera.instance.SetMapData(SceneData.instance.mapdata);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneData.instance.prevScene));
        SceneManager.UnloadSceneAsync(SceneData.instance.guardDungeon);
    }

    private void IncreaseWave()
    {
        wave++;
    }
}

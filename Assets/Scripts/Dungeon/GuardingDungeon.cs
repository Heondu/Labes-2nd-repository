using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GuardingDungeon : MonoBehaviour
{
    private RegenManager regenManager;
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

    private void Awake()
    {
        regenManager = FindObjectOfType<RegenManager>();
        tower = FindObjectOfType<Tower>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        regenManager.onRegen.AddListener(IncreaseWave);
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

        if (tower.status.HP == 0)// || player.status.HP == 0)
        {
            if (loadSceneOnDeath == false) return;

            player.transform.position = SceneData.instance.prevScenePos;
            SceneManager.LoadScene("MainScene");
        }
    }

    private void IncreaseWave()
    {
        wave++;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private AnimationController animationController;
    private new Collider2D collider2D;
    public PlayerStatus status = new PlayerStatus();
    public UnityEvent onLevelUp = new UnityEvent();
    [SerializeField]
    private float moveSpeed;
    private MapData mapData;

    public UnityEvent<string, int> onKillMonster = new UnityEvent<string, int>();
    public UnityEvent onDeath = new UnityEvent();

    [SerializeField]
    private GameObject dieMessage;
    private bool isDie = false;


    private void Awake()
    {
        movement = GetComponent<Movement>();
        animationController = GetComponent<AnimationController>();
        collider2D = GetComponent<Collider2D>();
        LoadStatus();

        LoadingSceneManager.onLevelWasLoaded.AddListener(RestoreHPOnLevelLoad);
}

    private void Update()
    {
        animationController.Movement(PlayerInput.instance.GetAxis());
        movement.Execute(PlayerInput.instance.GetAxis(), moveSpeed, mapData);

        status.CalculateDerivedStatus();
        LevelUp();
    }

    private void LevelUp()
    {
        if (status.exp >= (int)DataManager.experience[status.level]["exp"])
        {
            status.exp -= (int)DataManager.experience[status.level]["exp"];
            status.level++;
            onLevelUp.Invoke();
        }
    }

    public void TakeDamage(DamageData damageData)
    {
        int value = Mathf.RoundToInt(damageData.value);

        if (damageData.damageType == DamageType.miss) FloatingDamageManager.instance.FloatingDamage(gameObject, "Miss", transform.position, damageData.damageType);
        else FloatingDamageManager.instance.FloatingDamage(gameObject, value.ToString(), transform.position, damageData.damageType);

        if (damageData.damageType == DamageType.normal)
        {
            status.HP = Mathf.Max(0, status.HP - value);
        }
        else if (damageData.damageType == DamageType.critical)
        {
            status.HP = Mathf.Max(0, status.HP - value);
        }
        else if (damageData.damageType == DamageType.heal)
        {
            status.HP = Mathf.Min(status.HP + value, status.maxHP);
        }

        if (status.HP == 0 && isDie == false)
        {
            StartCoroutine("OnDeath");
        }
    }

    private IEnumerator OnDeath()
    {
        isDie = true;

        PlayerInput.SetInputMode(InputMode.pause);

        collider2D.enabled = false;

        dieMessage.SetActive(true);

        yield return new WaitForSeconds(3f);

        dieMessage.SetActive(false);

        onDeath.Invoke();
    }

    private void RestoreHPOnLevelLoad(string sceneName)
    {
        if (sceneName == SceneData.mainScene)
        {
            isDie = false;

            status.HP = status.maxHP;

            collider2D.enabled = true;

            PlayerInput.SetInputMode(InputMode.normal);
        }
    }

    public Status GetStatus(StatusList name)
    {
        return status.GetStatus(name);
    }
    public Status GetStatus(string name)
    {
        return status.GetStatus(name);
    }

    public object GetValue(StatusList name)
    {
        return status.GetValue(name);
    }

    public object GetValue(string name)
    {
        return status.GetValue(name);
    }

    public void SetMapData(MapData mapData)
    {
        this.mapData = mapData;
    }

    public void SaveStatus()
    {
        if (SaveDataManager.instance.saveStatus == false) return;
        JsonIO.SaveToJson(status, SaveDataManager.saveFile[SaveFile.PlayerStatus]);
    }

    public void LoadStatus()
    {
        if (SaveDataManager.instance.loadStatus)
        {
            status = JsonIO.LoadFromJson<PlayerStatus>(SaveDataManager.saveFile[SaveFile.PlayerStatus]);
            if (status == null)
            {
                status = new PlayerStatus();
                status.Init();
            }
        }
        else
        {
            status.Init();
        }
    }

    private void OnApplicationQuit()
    {
        SaveStatus();
    }
}

using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private AnimationController animationController;
    public PlayerStatus status;
    public UnityEvent onLevelUp = new UnityEvent();
    [SerializeField]
    private float moveSpeed;
    private MapData mapData;

    public UnityEvent<string, int> onKillMonster = new UnityEvent<string, int>();
    public UnityEvent onDeath = new UnityEvent();

    private void Awake()
    {
        movement = GetComponent<Movement>();
        animationController = GetComponent<AnimationController>();
        LoadStatus();
        status.HP = status.maxHP;
        status.mana = status.maxMana;
        status.exp = 0;
        status.level = 1;

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

        if (status.HP == 0)
        {
            onDeath.Invoke();
        }
    }

    private void RestoreHPOnLevelLoad(string sceneName)
    {
        if (sceneName == SceneData.instance.mainScene)
        {
            status.HP = status.maxHP;
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
        if (SaveDataManager.instance.loadStatus == false) return;
        status = JsonIO.LoadFromJson<PlayerStatus>(SaveDataManager.saveFile[SaveFile.PlayerStatus]);
    }
}

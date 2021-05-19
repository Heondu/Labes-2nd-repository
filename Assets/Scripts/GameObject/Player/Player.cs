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

    private void Awake()
    {
        movement = GetComponent<Movement>();
        animationController = GetComponent<AnimationController>();
        LoadStatus();
        status.HP = status.maxHP;
        status.mana = status.maxMana;
        status.exp = 0;
        status.level = 1;
}

    private void Update()
    {
        animationController.Movement(PlayerInput.instance.GetAxis());
        movement.Execute(PlayerInput.instance.GetAxis(), moveSpeed);

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

    public void TakeDamage(float _value, DamageType damageType, Vector3 hitDir)
    {
        int value = Mathf.RoundToInt(_value);

        if (damageType == DamageType.miss) FloatingDamageManager.instance.FloatingDamage(gameObject, "Miss", transform.position, damageType);
        else FloatingDamageManager.instance.FloatingDamage(gameObject, value.ToString(), transform.position, damageType);

        if (damageType == DamageType.normal)
        {
            status.HP = Mathf.Max(0, status.HP - value);
        }
        else if (damageType == DamageType.critical)
        {
            status.HP = Mathf.Max(0, status.HP - value);
        }
        else if (damageType == DamageType.heal)
        {
            status.HP = Mathf.Min(status.HP + value, status.maxHP);
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

    [ContextMenu("Save Status")]
    public void SaveStatus()
    {
        JsonIO.SaveToJson(status, SaveDataManager.saveFile[SaveFile.PlayerStatus]);
    }

    [ContextMenu("Load Status")]
    public void LoadStatus()
    {
        status = JsonIO.LoadFromJson<PlayerStatus>(SaveDataManager.saveFile[SaveFile.PlayerStatus]);
    }
}

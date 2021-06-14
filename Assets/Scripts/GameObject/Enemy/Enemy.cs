using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(LimbSeparator))]
[RequireComponent(typeof(Flash))]
[RequireComponent(typeof(Sorter))]
[RequireComponent(typeof(Minimap))]
public class Enemy : MonoBehaviour, ILivingEntity
{
    [SerializeField]
    private string id;
    private Movement movement;
    private EnemyController enemyController;
    private EnemyAttack enemyAttack;
    private AnimationController animationController;
    private new CapsuleCollider2D collider2D;
    public EnemyStatus status;
    public Dictionary<string, object> monster = new Dictionary<string, object>();
    public Dictionary<string, object> monlvl = new Dictionary<string, object>();
    private float delay;
    private Flash flash;
    private float hitTime = 0.5f;
    [SerializeField]
    private float moveSpeed;
    private MapData mapData;

    public UnityEvent onDeath = new UnityEvent();
    private UnityAction killCount = null;
    public Vector3 hitDir = Vector3.zero;

    private Player player;

    private string classType;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        enemyController = GetComponent<EnemyController>();
        enemyAttack = GetComponent<EnemyAttack>();
        animationController = GetComponent<AnimationController>();
        collider2D = GetComponent<CapsuleCollider2D>();
        flash = GetComponent<Flash>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        onDeath.AddListener(enemyController.OnDeath);
    }

    private void OnEnable()
    {
        if (killCount != null)
            onDeath.RemoveListener(killCount);
        collider2D.enabled = true;
        animationController.Enable(true);
    }

    private void Update()
    {
        switch (enemyController.GetState())
        {
            case EnemyState.STATE_PATROL:
                movement.Execute(enemyController.GetAxis(), moveSpeed, mapData);
                animationController.Movement(enemyController.GetAxis());
                break;
            case EnemyState.STATE_CHASE:
                movement.Execute(enemyController.GetAxis(), moveSpeed, mapData);
                animationController.Movement(enemyController.GetAxis());
                break;
            case EnemyState.STATE_ATTACK:
                Attack();
                break;
            case EnemyState.STATE_COMEBACK:
                movement.Execute(enemyController.GetAxis(), moveSpeed, mapData);
                animationController.Movement(enemyController.GetAxis());
                break;
        }
    }

    private void Attack()
    {
        if (enemyAttack.IsCool == false)
        {
            enemyAttack.Execute(delay, GetTarget(), status);
            animationController.Attack(enemyController.GetAttackDir());
            StartCoroutine(enemyController.Stop(1f));
        }
        else
        {
            movement.Execute(enemyController.GetAxis(), moveSpeed, mapData);
            animationController.Movement(enemyController.GetAxis());
        }
    }

    public void Init(UnityAction action, bool isElite)
    {
        monster = DataManager.monster.FindDic("name", id);
        monlvl = DataManager.monlvl.FindDic("Level", monster["monlvl"]);
        classType = monster["class"].ToString();
        status.level = (int)monster["monlvl"];
        status.maxHP = (int)monlvl["maxHP"];
        status.HP = status.maxHP;
        status.strength.BaseValue = (int)monlvl["strength"];
        status.agility.BaseValue = (int)monlvl["agility"];
        status.intelligence.BaseValue = (int)monlvl["intelligence"];
        status.endurance.BaseValue = (int)monlvl["endurance"];
        status.CalculateDerivedStatus();
        delay = float.Parse(monster["delay"].ToString());
        if (isElite) EliteMode();
        enemyAttack.SkillInit(monster, classType);

        if (action != null)
        {
            onDeath.AddListener(action);
            killCount = action;
        }
    }

    private void EliteMode()
    {
        classType = "elite";
        status.maxHP *= 2;
        status.HP = status.maxHP;
        status.strength.AddModifier(new StatusModifier(2, StatusModType.PercentMult));
        status.agility.AddModifier(new StatusModifier(2, StatusModType.PercentMult));
        status.intelligence.AddModifier(new StatusModifier(2, StatusModType.PercentMult));
        status.endurance.AddModifier(new StatusModifier(2, StatusModType.PercentMult));
        status.CalculateDerivedStatus();
        transform.localScale *= 1.5f;
    }

    public void TakeDamage(DamageData damageData)
    {
        enemyController.enemySwarmController.onSwarmAttackActive.Invoke();

        int value = Mathf.RoundToInt(damageData.value);

        if (damageData.damageType == DamageType.miss)
        {
            FloatingDamageManager.instance.FloatingDamage(gameObject, "Miss", transform.position, damageData.damageType);
            StartCoroutine(flash.Execute());
        }
        else
        {
            FloatingDamageManager.instance.FloatingDamage(gameObject, value.ToString(), transform.position, damageData.damageType);
        }

        if (damageData.damageType == DamageType.normal || damageData.damageType == DamageType.critical)
        {
            status.HP = Mathf.Max(0, status.HP - value);
            StartCoroutine(flash.Execute());
            StartCoroutine(enemyController.Stop(hitTime));
            if (damageData.damageType == DamageType.critical)
            {
                StartCoroutine(ShakeCamera.instance.Shake(0.05f, 0.3f));
            }
        }
        else if (damageData.damageType == DamageType.heal) status.HP = Mathf.Min(status.HP + value, status.maxHP);

        if (status.HP == 0)
        {
            FindObjectOfType<Player>().status.exp += (int)monlvl["monexp"];
            ItemGenerator.instance.DropItem(monlvl, classType, transform.position);
            hitDir = damageData.skillDir;
            collider2D.enabled = false;
            animationController.Enable(false);
            player.onKillMonster.Invoke(id, 1);
            onDeath.Invoke();
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

    public string GetID()
    {
        return id;
    }

    public Transform GetTarget()
    {
        return enemyController.GetTarget();
    }

    public void SetMapData(MapData mapData)
    {
        this.mapData = mapData;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Boss002 : MonoBehaviour, ILivingEntity
{
    public string id;

    public EnemyStatus status;
    private EnemyAttack enemyAttack;
    public Boss002Animator animator;
    private Flash flash;
    private Player player;

    public Dictionary<string, object> monster = new Dictionary<string, object>();
    public Dictionary<string, object> monlvl = new Dictionary<string, object>();

    private float delay;
    public bool CanAttack { private get; set; } = false;

    private void Awake()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        animator = GetComponent<Boss002Animator>();
        flash = GetComponent<Flash>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        Init();
        UIMonsterHP.instance.InitBossHPBar(transform, status, id);
    }

    private void Update()
    {
        animator.CheckForXPos(player.transform.position.x);

        Attack();
    }

    private void Attack()
    {
        if (enemyAttack.IsCool == false && CanAttack)
        {
            enemyAttack.Execute(delay, player.transform, status);
        }
    }

    public void Init()
    {
        monster = DataManager.monster.FindDic("name", id);
        monlvl = DataManager.monlvl.FindDic("Level", monster["monlvl"]);
        status.level = (int)monster["monlvl"];
        status.maxHP = 50;
        status.HP = status.maxHP;
        status.strength.BaseValue = (int)monlvl["strength"];
        status.agility.BaseValue = (int)monlvl["agility"];
        status.intelligence.BaseValue = (int)monlvl["intelligence"];
        status.endurance.BaseValue = (int)monlvl["endurance"];
        status.CalculateDerivedStatus();
        enemyAttack.SkillInit(monster);
        delay = float.Parse(monster["delay"].ToString());
    }

    public void TakeDamage(DamageData damageData)
    {
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
            if (damageData.damageType == DamageType.critical)
            {
                StartCoroutine(ShakeCamera.instance.Shake(0.05f, 0.3f));
            }
        }
        else if (damageData.damageType == DamageType.heal) status.HP = Mathf.Min(status.HP + value, status.maxHP);

        if (status.HP == 0)
        {
            FindObjectOfType<Player>().status.exp += (int)monlvl["monexp"];
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

    public Player GetPlayer()
    {
        return player;
    }
}

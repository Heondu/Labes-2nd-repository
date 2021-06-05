using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour, ILivingEntity
{
    [SerializeField]
    private UIBossHPViewer hpBar;

    public EnemyStatus status;

    private void Start()
    {
        status.HP = status.maxHP;

        hpBar.Init(transform, status, "Å¸¿ö");
    }

    public void TakeDamage(DamageData damageData)
    {
        if (damageData.executor.CompareTag("Player")) return;

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
    }

    public Status GetStatus(StatusList name)
    {
        return status.GetStatus(name);
    }
}

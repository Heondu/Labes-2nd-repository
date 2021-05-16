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

    public void TakeDamage(float _value, DamageType damageType)
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
}

using UnityEngine;

public class OreObject : MonoBehaviour, ILivingEntity
{
    public EnemyStatus status;
    [SerializeField]
    private int amount = 0;

    private void OnEnable()
    {
        status.HP = status.maxHP;
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

        if (status.HP == 0)
        {
            ItemGenerator.instance.DropOre(amount, transform.position);
            gameObject.SetActive(false);
        }
    }

    public Status GetStatus(StatusList name)
    {
        return status.GetStatus(name);
    }
}

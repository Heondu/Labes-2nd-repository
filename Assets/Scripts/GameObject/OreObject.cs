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

    public void TakeDamage(DamageData damageData)
    {
        int value = 1;

        FloatingDamageManager.instance.FloatingDamage(gameObject, value.ToString(), transform.position, DamageType.normal);

        status.HP = Mathf.Max(0, status.HP - value);

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

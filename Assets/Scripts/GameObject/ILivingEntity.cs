using UnityEngine;

public interface ILivingEntity
{
    void TakeDamage(DamageData damageData);

    Status GetStatus(StatusList name);
}

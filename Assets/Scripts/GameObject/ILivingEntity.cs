using UnityEngine;

public interface ILivingEntity
{
    void TakeDamage(float _value, DamageType damageType, Vector3 hitDir);

    Status GetStatus(StatusList name);
}

public interface ILivingEntity
{
    void TakeDamage(float _value, DamageType damageType);

    Status GetStatus(StatusList name);
}

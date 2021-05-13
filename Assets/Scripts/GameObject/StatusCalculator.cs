using UnityEngine;

public class StatusCalculator : MonoBehaviour
{
    public static void CalcSkillStatus(IStatus executor, ILivingEntity target, Skill skill, StatusList _status, StatusList _relatedStatus)
    {
        float value;
        Status relatedStatus = executor.GetStatus(_relatedStatus);
        Status status = target.GetStatus(_status);
        if (_relatedStatus == StatusList.none) value = skill.amount;
        else value = Mathf.RoundToInt(relatedStatus.Value * ((float)skill.amount / 100));
        
        if (skill.isPositive == 1)
        {
            if (_status == StatusList.HP) target.TakeDamage(value, DamageType.heal);
            else status.AddModifier(new StatusModifier(value, StatusModType.Flat, skill));
        }
        else if (skill.isPositive == 0)
        {
            if (_status == StatusList.HP)
            {
                bool isAvoid = IsAvoid(executor, target);
                if (isAvoid) target.TakeDamage(value, DamageType.miss);
                else
                {
                    value = relatedStatus.Value * skill.amount / (100 + target.GetStatus(StatusList.defence).Value);
                    value = Random.Range(value - (float)value % 2, value + (float)value % 2);
                    value = Mathf.Max(1, value);
                    if (Random.Range(0, 100) < executor.GetStatus(StatusList.critChance).Value) target.TakeDamage(value * 2, DamageType.critical);
                    else target.TakeDamage(value, DamageType.normal);
                }
            }
            else status.AddModifier(new StatusModifier(-value, StatusModType.Flat, skill));
        }
    }

    public static bool IsAvoid(IStatus executor, ILivingEntity target)
    {
        Status executorAccuracy = executor.GetStatus(StatusList.accuracy);
        Status targetAvoidance = target.GetStatus(StatusList.avoidance);

        float value = executorAccuracy.Value - targetAvoidance.Value + 100;

        if (value < Random.Range(0f, 100f)) return true;
        else return false;
    }
}

using UnityEngine;

public struct DamageData
{
    public GameObject executor;
    public float value;
    public DamageType damageType;
    public Vector3 skillDir;

    public DamageData(GameObject executor, float value, DamageType damageType, Vector3 skillDir)
    {
        this.executor = executor;
        this.value = value;
        this.damageType = damageType;
        this.skillDir = skillDir;
    }
}

public struct HitData
{
    public GameObject executorObj;
    public IStatus executor;
    public ILivingEntity target;
    public Skill skill;
    public StatusList status;
    public StatusList relatedStatus;
    public Vector3 skillDir;

    public HitData(GameObject executorObj, IStatus executor, ILivingEntity target, Skill skill, StatusList status, StatusList relatedStatus, Vector3 skillDir)
    {
        this.executorObj = executorObj;
        this.executor = executor;
        this.target = target;
        this.skill = skill;
        this.status = status;
        this.relatedStatus = relatedStatus;
        this.skillDir = skillDir;
    }
}

public class StatusCalculator : MonoBehaviour
{
    public static void CalcSkillStatus(HitData hitData)
    {
        float value;
        Status relatedStatus = hitData.executor.GetStatus(hitData.relatedStatus);
        Status status = hitData.target.GetStatus(hitData.status);
        if (hitData.relatedStatus == StatusList.none) value = hitData.skill.amount;
        else value = Mathf.RoundToInt(relatedStatus.Value * ((float)hitData.skill.amount / 100));
        
        if (hitData.skill.isPositive == 1)
        {
            if (hitData.status == StatusList.HP) hitData.target.TakeDamage(new DamageData(hitData.executorObj, value, DamageType.heal, hitData.skillDir));
            else status.AddModifier(new StatusModifier(value, StatusModType.Flat, hitData.skill));
        }
        else if (hitData.skill.isPositive == 0)
        {
            if (hitData.status == StatusList.HP)
            {
                bool isAvoid = IsAvoid(hitData.executor, hitData.target);
                if (isAvoid) hitData.target.TakeDamage(new DamageData(hitData.executorObj, value, DamageType.miss, hitData.skillDir));
                else
                {
                    value = relatedStatus.Value * hitData.skill.amount / (100 + hitData.target.GetStatus(StatusList.defence).Value);
                    value = Random.Range(value - (float)value % 2, value + (float)value % 2);
                    value = Mathf.Max(1, value);
                    if (Random.Range(0, 100) < hitData.executor.GetStatus(StatusList.critChance).Value)
                    {
                        hitData.target.TakeDamage(new DamageData(hitData.executorObj, value * 2, DamageType.critical, hitData.skillDir));
                    }
                    else
                    {
                        hitData.target.TakeDamage(new DamageData(hitData.executorObj, value, DamageType.normal, hitData.skillDir));
                    }
                }
            }
            else status.AddModifier(new StatusModifier(-value, StatusModType.Flat, hitData.skill));
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

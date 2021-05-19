using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillData))]
[RequireComponent(typeof(SkillEffectTrigger))]
public class SkillBuff : MonoBehaviour
{
    [SerializeField]
    protected float radius;
    protected List<ILivingEntity> entityList = new List<ILivingEntity>();
    protected Transform buffHolder;
    protected Transform buffUIHolder;
    protected GameObject buffPrefab;
    protected SkillData skillData;
    protected SkillLifetime skillLifetime;
    protected SkillEffectTrigger skillEffectTrigger;

    protected virtual void Awake()
    {
        if (GameObject.Find("BuffHolder") == null)
        {
            buffHolder = new GameObject("BuffHolder").transform;
        }
        else
        {
            buffHolder = GameObject.Find("BuffHolder").transform;
        }

        buffUIHolder = GameObject.Find("Buff").transform;
        skillData = GetComponent<SkillData>();
        skillLifetime = GetComponent<SkillLifetime>();
        buffPrefab = Resources.Load<GameObject>("Prefabs/UI/Buff");
        skillEffectTrigger = GetComponent<SkillEffectTrigger>();
    }

    protected virtual void Start()
    {
        skillEffectTrigger.onStart.Invoke();

        Transform buff = buffHolder.Find(gameObject.name);
        if (buff != null)
        {
            buff.GetComponent<SkillLifetime>().ResetTime();
            Destroy(gameObject);
        }
        else
        {
            transform.SetParent(buffHolder);
            Execute();
        }
    }

    protected virtual void Execute()
    {
        Skill skill = skillData.skill;
        List<GameObject> targets = new List<GameObject>();
        if (skill.isPositive == 1)
        {
            targets.Add(skillData.executor);
        }
        else if (skill.isPositive == 0)
        {
            targets = FindAllTarget(radius);
        }
        foreach (GameObject target in targets)
        {
            ILivingEntity targetEntity = target.GetComponent<ILivingEntity>();
            entityList.Add(targetEntity);
            for (int i = 0; i < skill.repeat; i++)
            {
                CreateNextSkill();

                skillEffectTrigger.SetTarget(target.transform);
                skillEffectTrigger.onHit.Invoke();

                StatusCalculator.CalcSkillStatus(skillData.executorStatus, targetEntity, skill, skillData.GetStatus, skillData.GetRelatedStatus, Vector3.zero);
            }
        }

        GameObject clone = Instantiate(buffPrefab, buffUIHolder);
        clone.GetComponent<UIBuffLifetimeViewer>().Init(skill, skillLifetime);
    }

    private void CreateNextSkill()
    {
        for (int i = 0; i < skillData.nextSkills.Length; i++)
        {
            SkillLoader.instance.LoadSkill(skillData, skillData.nextSkills[i], transform.position, transform.up);
        }
    }

    protected List<GameObject> FindAllTarget(float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        List<GameObject> targetList = new List<GameObject>();
        foreach (Collider2D collider in colliders)
        {
            ILivingEntity entity = collider.GetComponent<ILivingEntity>();

            if (entity == null) continue;
            if (collider.gameObject.CompareTag(skillData.executor.tag) == true) continue;

            targetList.Add(collider.gameObject);
        }
        return targetList;
    }

    private void OnDestroy()
    {
        foreach (ILivingEntity entity in entityList)
        {
            for (int i = 0; i < 2; i++)
            {
                Status status = entity.GetStatus(skillData.GetStatus);
                if (status != null) status.RemoveAllModifiersFromSource(skillData.skill);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

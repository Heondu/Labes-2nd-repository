using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillData))]
[RequireComponent(typeof(SkillEffectTrigger))]
public class SkillExplode : MonoBehaviour
{
    [SerializeField]
    protected float radius = 1;
    [SerializeField]
    protected float delay = 1f;
    [SerializeField]
    protected bool singleTarget;
    protected int penetrationCount = 0;
    protected SkillData skillData;
    protected SkillEffectTrigger skillEffectTrigger;

    protected virtual void Awake()
    {
        skillData = GetComponent<SkillData>();
        skillEffectTrigger = GetComponent<SkillEffectTrigger>();
    }

    protected virtual void Start()
    {
        skillEffectTrigger.onStart.Invoke();

        StartCoroutine(CoExecute());
    }

    private IEnumerator CoExecute()
    {
        while (true)
        {
            Execute();
            yield return new WaitForSeconds(delay);
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
            if (singleTarget) targets.Add(FindTarget(radius));
            else targets = FindAllTarget(radius);
        }
        foreach (GameObject target in targets)
        {
            if (target == null) continue;
            ILivingEntity targetEntity = target.GetComponent<ILivingEntity>();
            for (int i = 0; i < skillData.AttackNum; i++)
            {
                CreateNextSkill();

                skillEffectTrigger.SetTarget(target.transform);
                skillEffectTrigger.onHit.Invoke();

                StatusCalculator.CalcSkillStatus(skillData.executorStatus, targetEntity, skill, skillData.GetStatus, skillData.GetRelatedStatus);
            }
            penetrationCount++;
            if (penetrationCount >= skillData.penetration)
            {
                Destroy(gameObject);
            }
        }
    }

    private void CreateNextSkill()
    {
        for (int i = 0; i < skillData.nextSkills.Length; i++)
        {
            SkillLoader.instance.LoadSkill(skillData, skillData.nextSkills[i], transform.position, transform.up);
        }
    }

    protected GameObject FindTarget(float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        GameObject target = null;
        float distance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            ILivingEntity entity = collider.GetComponent<ILivingEntity>();

            if (entity == null) continue;
            if (collider.gameObject.CompareTag(skillData.executor.tag) == true) continue;

            float newDistance = Vector3.SqrMagnitude(collider.transform.position - transform.position);
            if (distance > newDistance)
            {
                target = collider.gameObject;
                distance = newDistance;
            }
        }
        return target;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

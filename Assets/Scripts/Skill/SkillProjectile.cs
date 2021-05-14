using UnityEngine;

[RequireComponent(typeof(SkillData))]
[RequireComponent(typeof(ProjectileMove))]
[RequireComponent(typeof(SkillEffectTrigger))]
public class SkillProjectile : MonoBehaviour
{
    protected GameObject target;
    protected int penetrationCount = 0;
    protected SkillData skillData;
    protected ProjectileMove projectileMove;
    protected SkillEffectTrigger skillEffectTrigger;

    protected virtual void Awake()
    {
        skillData = GetComponent<SkillData>();
        projectileMove = GetComponent<ProjectileMove>();
        skillEffectTrigger = GetComponent<SkillEffectTrigger>();
    }

    protected virtual void Start()
    {
        skillEffectTrigger.onStart.Invoke();

        projectileMove.SetSpeed(skillData.speed);
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Execute()
    {
        CreateNextSkill();
    }

    private void CreateNextSkill()
    {
        for (int i = 0; i < skillData.nextSkills.Length; i++)
        {
            SkillLoader.instance.LoadSkill(skillData, skillData.nextSkills[i], transform.position, transform.up);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(skillData.targetTag))
        {
            skillEffectTrigger.SetTarget(collision.transform);
            skillEffectTrigger.onHit.Invoke();

            Execute();

            penetrationCount++;
            if (penetrationCount >= skillData.penetration)
            {
                Destroy(gameObject);
            }
        }
    }
}

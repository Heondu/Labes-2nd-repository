using UnityEngine;

[RequireComponent(typeof(ProjectileMove))]
[RequireComponent(typeof(SkillData))]
public class GuidedProjectile : MonoBehaviour
{
    [SerializeField]
    private float radius;
    private GameObject target = null;
    private ProjectileMove projectileMove;
    private SkillData skillData;

    private void Awake()
    {
        projectileMove = GetComponent<ProjectileMove>();
        skillData = GetComponent<SkillData>();
    }

    private void Update()
    {
        target = FindTarget(radius);
        if (target != null)
        {
            Vector2 dir = target.transform.position - transform.position;
            projectileMove.SetDir(dir.normalized, skillData.skill.guide);
        }
    }

    private GameObject FindTarget(float radius)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

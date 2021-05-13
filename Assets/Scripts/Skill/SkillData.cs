using UnityEngine;

[RequireComponent(typeof(SkillLifetime))]
public class SkillData : MonoBehaviour
{
    [SerializeField]
    private StatusList status;
    [SerializeField]
    private StatusList relatedStatus;
    public StatusList GetStatus => status;
    public StatusList GetRelatedStatus => relatedStatus;
    public Skill skill { get; private set; }
    public GameObject executor { get; private set; }
    public IStatus executorStatus { get; private set; }
    public string targetTag { get; private set; }
    public float damage { get; private set; }
    public float size { get; private set; }
    [SerializeField]
    private int attackNum = 1;
    public int AttackNum => attackNum;
    public float speed { get; private set; }
    public float lifetime { get; private set; }
    public int penetration { get; private set; }

    public GameObject[] nextSkills;

    public void Init(GameObject executor, IStatus status, string targetTag, Skill skill, GameObject[] nextSkills)
    {
        this.skill = skill;
        this.executor = executor;
        executorStatus = status;
        this.targetTag = targetTag;
        damage = status.GetStatus(StatusList.damage).Value;
        size = skill.size;
        speed = skill.speed;
        lifetime = skill.lifetime;
        penetration = skill.penetration;
        this.nextSkills = nextSkills;
    }

    public void InitChild(GameObject executor, IStatus status, string targetTag, Skill skill)
    {
        SkillData[] skillDatas = GetComponentsInChildren<SkillData>();
        for (int i = 0; i < skillDatas.Length; i++)
        {
            skillDatas[i].Init(executor, status, targetTag, skill, nextSkills);
        }
    }
}

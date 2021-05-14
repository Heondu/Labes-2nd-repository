using System.Collections;
using UnityEngine;

public class SkillLoader : MonoBehaviour
{
    public static SkillLoader instance;
    private Transform skillHolder;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;

        skillHolder = new GameObject("SkillHolder").transform;
    }

    public void LoadSkill(GameObject executor, IStatus executorStatus, string targetTag, Skill skill, Vector3 pos, Vector3 dir)
    {
        GameObject skillObjcet = Resources.Load("Prefabs/Skills/" + skill.name) as GameObject;
        StartCoroutine(Create(executor, executorStatus, targetTag, skill, skillObjcet, pos, dir));
    }

    public void LoadSkill(SkillData skillData, Skill skill, Vector3 pos, Vector3 dir)
    {
        GameObject skillObjcet = Resources.Load("Prefabs/Skills/" + skill.name) as GameObject;
        StartCoroutine(Create(skillData.executor, skillData.executorStatus, skillData.targetTag, skill, skillObjcet, pos, dir));
    }

    public void LoadSkill(SkillData skillData, GameObject skillObjcet, Vector3 pos, Vector3 dir)
    {
        Skill skill = DataManager.skillDB[skillObjcet.name];
        StartCoroutine(Create(skillData.executor, skillData.executorStatus, skillData.targetTag, skill, skillObjcet, pos, dir));
    }

    private IEnumerator Create(GameObject executor, IStatus executorStatus, string targetTag, Skill skill, GameObject skillObjcet, Vector3 pos, Vector3 dir)
    {
        if (skill.position == "pointer") pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else if (skill.position == "hero") pos += dir.normalized;
        pos.z = 0;
        
        for (int i = 0; i < skill.repeat; i++)
        {
            //GameObject clone = ObjectPooler.instance.ObjectPool(skillHolder, skillObjcet);
            //clone.transform.SetPositionAndRotation(pos, Quaternion.AngleAxis(Rotation.GetAngle(dir), Vector3.forward));
            GameObject clone = Instantiate(skillObjcet, pos, Quaternion.AngleAxis(Rotation.GetAngle(dir), Vector3.forward));
            clone.GetComponent<SkillData>().InitChild(executor, executorStatus, targetTag, skill);

            yield return new WaitForSeconds(0.1f);
        }
    }
}

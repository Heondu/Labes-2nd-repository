using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool isCool = false;
    public bool IsCool => isCool;
    public List<Skill> skillList = new List<Skill>();
    public List<int> probList = new List<int>();
    public Dictionary<Skill, bool> isSkillCool = new Dictionary<Skill, bool>();
    public Dictionary<Skill, Timer> skillCool = new Dictionary<Skill, Timer>();
    private int sumOfProb = 0;

    private void OnEnable()
    {
        isCool = true;
        StartCoroutine("Delay", Random.Range(1f, 3f));
        isSkillCool.Clear();
        skillCool.Clear();
    }

    private bool IsAttack(Skill skill)
    {
        if (isCool) return false;
        if (skill == null) return false;
        if (isSkillCool[skill]) return false;
        return true;
    }

    public void Execute(float delay, Transform target, EnemyStatus status)
    {
        Skill skill = SelectRandomSkill();

        if (!IsAttack(skill)) return;

        SkillLoader.instance.LoadSkill(gameObject, status, skill, transform.position, (target.position - transform.position).normalized);
        isCool = true;
        isSkillCool[skill] = true;
        StartCoroutine("Cooltime", skill);
        StartCoroutine("Delay", delay);
    }

    private IEnumerator Cooltime(Skill skill)
    {
        while (skillCool[skill].IsTimeOut(skill.cooltime) == false)
        {
            yield return null;
        }
        isSkillCool[skill] = false;
    }

    private IEnumerator Delay(float delay)
    {
        Timer timer = new Timer();
        while (timer.IsTimeOut(delay) == false)
        {
            yield return null;
        }
        isCool = false;
    }

    public void SkillInit(Dictionary<string, object> monster, string classType)
    {
        if (classType == "elite")
        {
            Skill skill = SkillManager.instance.GetEliteSkill();
            skillList.Add(skill);
            probList.Add(100);
            sumOfProb += 100;
            isSkillCool[skill] = false;
            skillCool[skill] = new Timer();
            return;
        }

        for (int i = 1; i <= 6; i++)
        {
            if (monster["skill" + i].ToString() != "")
            {
                Skill skill = DataManager.skillDB[monster["skill" + i].ToString()];
                skillList.Add(skill);
                probList.Add((int)monster["prob" + i]);
                isSkillCool[skill] = false;
                skillCool[skill] = new Timer();
            }
        }

        for (int i = 0; i < probList.Count; i++)
        {
            sumOfProb += probList[i];
        }
    }

    private Skill SelectRandomSkill()
    {
        int rand = Random.Range(0, sumOfProb);
        int sum = 0;
        int index = 0;
        for (int i = 0; i < probList.Count; i++)
        {
            sum += probList[i];
            if (rand < sum)
            {
                index = i;
                break;
            }
        }

        return skillList[index];
    }
}

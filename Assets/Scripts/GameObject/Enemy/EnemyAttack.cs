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
    private Transform player;
    private Enemy enemy;
    private int sumOfProb = 0;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        enemy = GetComponent<Enemy>();
    }

    private bool IsAttack(Skill skill)
    {
        if (isCool) return false;
        if (skill == null) return false;
        if (isSkillCool[skill]) return false;
        return true;
    }

    public void Execute(float delay)
    {
        Skill skill = SelectRandomSkill();

        if (!IsAttack(skill)) return;

        SkillLoader.instance.LoadSkill(gameObject, enemy.status, "Player", skill, transform.position, (player.position - transform.position).normalized);
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

    private IEnumerator Delay(int delay)
    {
        Timer timer = new Timer();
        while (timer.IsTimeOut(delay) == false)
        {
            yield return null;
        }
        isCool = false;
    }

    public void SkillInit(Dictionary<string, object> monster)
    {
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

    private void SkillSort()
    {
        for (int i = skillList.Count - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (probList[j] < probList[j + 1])
                {
                    int tempProb = probList[j + 1];
                    probList[j + 1] = probList[j];
                    probList[j] = tempProb;
                    Skill tempSkill = skillList[j + 1];
                    skillList[j + 1] = skillList[j];
                    skillList[j] = tempSkill;
                }
            }
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

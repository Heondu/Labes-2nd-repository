using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [SerializeField]
    private GameObject[] eliteSkills;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public Skill GetEliteSkill()
    {
        int rand = Random.Range(0, eliteSkills.Length);

        return DataManager.skillDB[eliteSkills[rand].name];
    }
}

using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [SerializeField]
    private GameObject[] eliteSkills;

    [SerializeField]
    private string[] playerBaseSkills;

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

    public string[] GetPlayerBaseSkills()
    {
        return playerBaseSkills;
    }
}

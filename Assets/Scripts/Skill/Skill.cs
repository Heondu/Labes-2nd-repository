public class Skill
{
    public string skill;
    public string name;
    public int rarity;
    public int exp;
    public string material;
    public string weaponClass;
    public string classBonus;
    public int bonusAmount;
    public string position;
    public string element;
    public float cooltime;
    public int amount;
    public int perlvl;
    public int isPositive;
    public int repeat;
    public int speed;
    public int size;
    public int lifetime;
    public float guide;
    public int penetration;
    public int quality = 0;
    public bool isNew = true;
}

[System.Serializable]
public class SkillSaveData
{
    public string skill;
    public string name;
    public int rarity;
    public int exp;
    public string material;
    public string weaponClass;
    public string classBonus;
    public int bonusAmount;
    public string position;
    public string element;
    public float cooltime;
    public int amount;
    public int perlvl;
    public int isPositive;
    public int repeat;
    public int speed;
    public int size;
    public int lifetime;
    public float guide;
    public int penetration;
    public int quality = 0;
    public bool isNew = true;

    public SkillSaveData(Skill skill)
    {
        if (skill == null) return;

        this.skill = skill.skill;
        name = skill.name;
        rarity = skill.rarity;
        exp = skill.exp;
        material = skill.material;
        weaponClass = skill.weaponClass;
        classBonus = skill.classBonus;
        bonusAmount = skill.bonusAmount;
        position = skill.position;
        element = skill.element;
        cooltime = skill.cooltime;
        amount = skill.amount;
        perlvl = skill.perlvl;
        isPositive = skill.isPositive;
        repeat = skill.repeat;
        speed = skill.speed;
        size = skill.size;
        lifetime = skill.lifetime;
        guide = skill.guide;
        penetration = skill.perlvl;
        quality = skill.quality;
        isNew = skill.isNew;
    }

    public Skill DeepCopy()
    {
        Skill skill = new Skill();

        skill.skill = this.skill;
        skill.name = name;
        skill.rarity = rarity;
        skill.exp = exp;
        skill.material = material;
        skill.weaponClass = weaponClass;
        skill.classBonus = classBonus;
        skill.bonusAmount = bonusAmount;
        skill.position = position;
        skill.element = element;
        skill.cooltime = cooltime;
        skill.amount = amount;
        skill.perlvl = perlvl;
        skill.isPositive = isPositive;
        skill.repeat = repeat;
        skill.speed = speed;
        skill.size = size;
        skill.lifetime = lifetime;
        skill.guide = guide;
        skill.penetration = perlvl;
        skill.quality = quality;
        skill.isNew = isNew;

        return skill;
    }
}

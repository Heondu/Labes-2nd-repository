public interface IStatus
{
    public void CalculateDerivedStatus();

    public Status GetStatus(StatusList name);
    public Status GetStatus(string name);
}

public enum StatusList
{
    none,
    HP,
    maxHP,
    mana,
    maxMana,
    exp,
    level,
    strength,
    agility,
    intelligence,
    endurance,
    damage,
    defence,
    allResist,
    fireResist,
    coldResist,
    darkResist,
    lightResist,
    fireDamage,
    coldDamage,
    darkDamage,
    lightDamage,
    fixDamage,
    critChance,
    critResist,
    critDamage,
    avoidance,
    accuracy,
    reduceMana,
    reduceCool,
    experience,
    itemRange
}

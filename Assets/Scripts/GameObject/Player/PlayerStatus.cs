using System;

[System.Serializable]
public class PlayerStatus : IStatus
{
    public int HP = 0;
    public int maxHP = 100;
    public int mana = 0;
    public int maxMana = 100;
    public float exp = 0;
    public int level = 1;

    public Status strength = new Status();
    public Status agility = new Status();
    public Status intelligence = new Status();
    public Status endurance = new Status();
    public Status damage = new Status();
    public Status defence = new Status();
    public Status allResist = new Status();
    public Status fireResist = new Status();
    public Status coldResist = new Status();
    public Status darkResist = new Status();
    public Status lightResist = new Status();
    public Status fireDamage = new Status();
    public Status coldDamage = new Status();
    public Status darkDamage = new Status();
    public Status lightDamage = new Status();
    public Status fixDamage = new Status();
    public Status critChance = new Status();
    public Status critResist = new Status();
    public Status critDamage = new Status();
    public Status avoidance = new Status();
    public Status accuracy = new Status();
    public Status reduceMana = new Status();
    public Status reduceCool = new Status();
    public Status experience = new Status();
    public Status itemRange = new Status();

    private const float multValue = 0.05f;

    public void CalculateDerivedStatus()
    {
        damage.RemoveAllModifiersFromSource(strength);
        fixDamage.RemoveAllModifiersFromSource(strength);
        critChance.RemoveAllModifiersFromSource(agility);
        avoidance.RemoveAllModifiersFromSource(agility);
        accuracy.RemoveAllModifiersFromSource(agility);
        reduceMana.RemoveAllModifiersFromSource(intelligence);
        reduceCool.RemoveAllModifiersFromSource(intelligence);
        defence.RemoveAllModifiersFromSource(endurance);
        allResist.RemoveAllModifiersFromSource(endurance);

        damage.AddModifier(new StatusModifier(strength.Value, StatusModType.Flat, strength));
        fixDamage.AddModifier(new StatusModifier(strength.Value, StatusModType.Flat, strength));
        critChance.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat, agility));
        avoidance.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat, agility));
        accuracy.AddModifier(new StatusModifier(agility.Value, StatusModType.Flat, agility));
        reduceMana.AddModifier(new StatusModifier(intelligence.Value, StatusModType.Flat, intelligence));
        reduceCool.AddModifier(new StatusModifier(intelligence.Value, StatusModType.Flat, intelligence));
        defence.AddModifier(new StatusModifier(endurance.Value, StatusModType.Flat, endurance));
        allResist.AddModifier(new StatusModifier(endurance.Value, StatusModType.Flat, endurance));

        fixDamage.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, strength));
        critChance.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, agility));
        avoidance.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, agility));
        accuracy.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, agility));
        reduceMana.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, intelligence));
        reduceCool.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, intelligence));
        allResist.AddModifier(new StatusModifier(multValue, StatusModType.PercentAdd, endurance));
    }

    public Status GetStatus(StatusList name)
    {
        switch (name)
        {
            case StatusList.strength: return strength;
            case StatusList.agility: return agility;
            case StatusList.intelligence: return intelligence;
            case StatusList.endurance: return endurance;
            case StatusList.damage: return damage;
            case StatusList.defence: return defence;
            case StatusList.allResist: return allResist;
            case StatusList.fireResist: return fireResist;
            case StatusList.coldResist: return coldResist;
            case StatusList.darkResist: return darkResist;
            case StatusList.lightResist: return lightResist;
            case StatusList.fireDamage: return fireDamage;
            case StatusList.coldDamage: return coldDamage;
            case StatusList.darkDamage: return darkDamage;
            case StatusList.lightDamage: return lightDamage;
            case StatusList.fixDamage: return fixDamage;
            case StatusList.critChance: return critChance;
            case StatusList.critResist: return critResist;
            case StatusList.critDamage: return critDamage;
            case StatusList.avoidance: return avoidance;
            case StatusList.accuracy: return accuracy;
            case StatusList.reduceMana: return reduceMana;
            case StatusList.reduceCool: return reduceCool;
            case StatusList.experience: return experience;
            case StatusList.itemRange: return itemRange;
        }
        return null;
    }

    public Status GetStatus(string name)
    {
        if (name.Contains("%"))
        {
            name = name.Substring(0, name.Length - 1);
        }
        return GetStatus((StatusList)Enum.Parse(typeof(StatusList), name));
    }

    public object GetValue(StatusList name)
    {
        switch (name)
        {
            case StatusList.HP: return HP;
            case StatusList.maxHP: return maxHP;
            case StatusList.mana: return mana;
            case StatusList.maxMana: return maxMana;
            case StatusList.exp: return exp;
            case StatusList.level: return level;
        }
        return null;
    }

    public object GetValue(string name)
    {
        return GetValue((StatusList)Enum.Parse(typeof(StatusList), name));
    }

    public void Init()
    {
        maxHP = 100;
        HP = maxHP;
        maxMana = 100;
        mana = maxMana;
        exp = 0;
        level = 1;

        strength.BaseValue = 10;
        agility.BaseValue = 10;
        intelligence.BaseValue = 10;
        endurance.BaseValue = 10;
        damage.BaseValue = 0;
        defence.BaseValue = 0;
        allResist.BaseValue = 0;
        fireResist.BaseValue = 0;
        coldResist.BaseValue = 0;
        darkResist.BaseValue = 0;
        lightResist.BaseValue = 0;
        fireDamage.BaseValue = 0;
        coldDamage.BaseValue = 0;
        darkDamage.BaseValue = 0;
        lightDamage.BaseValue = 0;
        fixDamage.BaseValue = 0;
        critChance.BaseValue = 0;
        critResist.BaseValue = 0;
        critDamage.BaseValue = 0;
        avoidance.BaseValue = 0;
        accuracy.BaseValue = 0;
        reduceMana.BaseValue = 0;
        reduceCool.BaseValue = 0;
        experience.BaseValue = 0;
        itemRange.BaseValue = 2;
    }
}
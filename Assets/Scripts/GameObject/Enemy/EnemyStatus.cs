using System;

[System.Serializable]
public class EnemyStatus : IStatus
{
    public int level;
    public int HP;
    public int maxHP;

    public Status strength;
    public Status agility;
    public Status intelligence;
    public Status endurance;
    public Status damage;
    public Status defence;
    public Status allResist;
    public Status fireResist;
    public Status coldResist;
    public Status darkResist;
    public Status lightResist;
    public Status fireDamage;
    public Status coldDamage;
    public Status darkDamage;
    public Status lightDamage;
    public Status fixDamage;
    public Status critChance;
    public Status critResist;
    public Status critDamage;
    public Status avoidance;
    public Status accuracy;
    public Status reduceMana;
    public Status reduceCool;

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
        }
        return null;
    }

    public Status GetStatus(string name)
    {
        return GetStatus((StatusList)Enum.Parse(typeof(StatusList), name));
    }
}

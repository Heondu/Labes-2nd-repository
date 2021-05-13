using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class Status
{
    public float BaseValue;

    public virtual float Value 
    {
        get
        {
            if (isDirty || BaseValue != lastBaseValue)
            {
                lastBaseValue = BaseValue;
                value = CalculateFinalValue();
                isDirty = false;
            }
            return value;
        } 
    }

    protected bool isDirty = true;
    protected float value;
    protected float lastBaseValue = float.MinValue;

    protected readonly List<StatusModifier> statusModifiers;
    public readonly ReadOnlyCollection<StatusModifier> StatusModifiers;

    public Status()
    {
        statusModifiers = new List<StatusModifier>();
        StatusModifiers = statusModifiers.AsReadOnly();
    }

    public Status(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    public virtual void AddModifier(StatusModifier mod)
    {
        isDirty = true;
        statusModifiers.Add(mod);
        statusModifiers.Sort(CompareModifierOrder);
    }

    public virtual bool RemoveModifier(StatusModifier mod)
    {
        if (statusModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statusModifiers.Count - 1; i >= 0; i--)
        {
            if (statusModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statusModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual int CompareModifierOrder(StatusModifier a, StatusModifier b)
    {
        if (a.Order < b.Order) return -1;
        else if (a.Order > b.Order) return 1;
        return 0;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statusModifiers.Count; i++)
        {
            StatusModifier mod = statusModifiers[i];

            if (mod.Type == StatusModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatusModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;

                if (i + 1 >= statusModifiers.Count || statusModifiers[i + 1].Type != StatusModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.Type == StatusModType.PercentMult)
            {
                finalValue *= mod.Value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }
}
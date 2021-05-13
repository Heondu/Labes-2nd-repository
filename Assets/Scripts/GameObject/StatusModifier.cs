public enum StatusModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

public class StatusModifier
{
    public readonly float Value;
    public readonly StatusModType Type;
    public readonly int Order;
    public readonly object Source;

    public StatusModifier(float value, StatusModType type, int order, object source)
    {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }

    public StatusModifier(float value, StatusModType type) : this(value, type, (int)type, null) { }

    public StatusModifier(float value, StatusModType type, int order) : this(value, type, order, null) { }

    public StatusModifier(float value, StatusModType type, object source) : this(value, type, (int)type, source) { }
}

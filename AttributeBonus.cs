namespace DotNetLessons;

public enum BonusType
{
    Add,
    Multiply
}

public class AttributeBonus
{
    public BonusType Type { get; set; }
    public float Value { get; set; }

    public AttributeBonus(BonusType type, float value)
    {
        Type = type;
        Value = value;
    }
}
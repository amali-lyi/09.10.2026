using System;
using System.Collections.Generic;

namespace DotNetLessons;

public delegate void ValueChangedHandler(float oldValue, float newValue);
public delegate void BonusHandler(AttributeBonus bonus);

public class Attribute
{
    private float baseValue;

    public float BaseValue
    {
        get { return baseValue; }
        set
        {
            float oldValue = Value;
            baseValue = value;
            CheckValueChanged(oldValue);
        }
    }

    public List<AttributeBonus> Bonuses { get; set; }

    public float? MinValue { get; set; }
    public float? MaxValue { get; set; }

    public float Value
    {
        get
        {
            float addBonus = 0f;
            float multiplyBonus = 1f;

            foreach (AttributeBonus bonus in Bonuses)
            {
                if (bonus.Type == BonusType.Add)
                {
                    addBonus += bonus.Value;
                }
                else if (bonus.Type == BonusType.Multiply)
                {
                    multiplyBonus *= bonus.Value;
                }
            }

            float result = (BaseValue + addBonus) * multiplyBonus;

            if (MinValue.HasValue && result < MinValue.Value)
            {
                result = MinValue.Value;
            }

            if (MaxValue.HasValue && result > MaxValue.Value)
            {
                result = MaxValue.Value;
            }

            return result;
        }
    }

    public event ValueChangedHandler? OnValueChanged;
    public event BonusHandler? OnBonusAdded;
    public event BonusHandler? OnBonusRemoved;

    public Attribute(float baseValue, float? minValue = null, float? maxValue = null)
    {
        this.baseValue = baseValue;
        MinValue = minValue;
        MaxValue = maxValue;
        Bonuses = new List<AttributeBonus>();
    }

    private void CheckValueChanged(float oldValue)
    {
        float newValue = Value;

        if (oldValue != newValue)
        {
            OnValueChanged?.Invoke(oldValue, newValue);
        }
    }

    public void AddBonus(AttributeBonus bonus)
    {
        float oldValue = Value;

        Bonuses.Add(bonus);
        OnBonusAdded?.Invoke(bonus);

        CheckValueChanged(oldValue);
    }

    public void RemoveBonus(AttributeBonus bonus)
    {
        float oldValue = Value;

        if (Bonuses.Remove(bonus))
        {
            OnBonusRemoved?.Invoke(bonus);
            CheckValueChanged(oldValue);
        }
    }
}
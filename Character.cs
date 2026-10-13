using System;

namespace DotNetLessons;

public class Character
{
    public Attribute Health { get; set; }
    public Attribute Stamina { get; set; }
    public Attribute Strength { get; set; }

    public event Action? OnDead;
    public event Action? OnExhausted;
    public event Action<float, float>? OnStrengthChanged;

    public Character()
    {
        Health = new Attribute(100f, 0f, 100f);
        Stamina = new Attribute(100f, 0f, 100f);
        Strength = new Attribute(10f, 0f);

        Health.OnValueChanged += (oldValue, newValue) =>
        {
            if (oldValue > 0f && newValue == 0f)
            {
                OnDead?.Invoke();
            }
        };

        Stamina.OnValueChanged += (oldValue, newValue) =>
        {
            if (oldValue > 0f && newValue == 0f)
            {
                OnExhausted?.Invoke();
            }
        };

        Strength.OnValueChanged += (oldValue, newValue) =>
        {
            OnStrengthChanged?.Invoke(oldValue, newValue);
        };
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0f)
        {
            Health.BaseValue -= damage;

            if (Health.BaseValue < 0f)
            {
                Health.BaseValue = 0f;
            }
        }
    }

    public void UseStamina(float amount)
    {
        if (amount > 0f)
        {
            Stamina.BaseValue -= amount;

            if (Stamina.BaseValue < 0f)
            {
                Stamina.BaseValue = 0f;
            }
        }
    }

    public void AddStrengthBonus(AttributeBonus bonus)
    {
        Strength.AddBonus(bonus);
    }
}
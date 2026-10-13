using System;

namespace DotNetLessons;

public class Program
{
    public static void Main()
    {
        Character character = new Character();

        character.OnDead += () =>
        {
            Console.WriteLine("the character has died!");
        };

        character.OnExhausted += () =>
        {
            Console.WriteLine("the character is exhausted!");
        };

        character.OnStrengthChanged += (oldValue, newValue) =>
        {
            Console.WriteLine($"strength changed: {oldValue} -> {newValue}");
        };

        character.Health.OnValueChanged += (oldValue, newValue) =>
        {
            Console.WriteLine($"health changed: {oldValue} -> {newValue}");
        };

        character.Stamina.OnValueChanged += (oldValue, newValue) =>
        {
            Console.WriteLine($"stamina changed: {oldValue} -> {newValue}");
        };

        character.Strength.OnBonusAdded += bonus =>
        {
            Console.WriteLine($"bonus added: {bonus.Type}, {bonus.Value}");
        };

        character.Strength.OnBonusRemoved += bonus =>
        {
            Console.WriteLine($"bonus removed: {bonus.Type}, {bonus.Value}");
        };

        Console.WriteLine("=== initial values ===");
        Console.WriteLine($"health: {character.Health.Value}");
        Console.WriteLine($"stamina: {character.Stamina.Value}");
        Console.WriteLine($"strength: {character.Strength.Value}");

        Console.WriteLine("\n=== adding bonuses ===");

        AttributeBonus bonus1 = new AttributeBonus(BonusType.Add, 5f);
        AttributeBonus bonus2 = new AttributeBonus(BonusType.Multiply, 2f);
        AttributeBonus bonus3 = new AttributeBonus(BonusType.Add, 3f);

        character.AddStrengthBonus(bonus1);
        character.AddStrengthBonus(bonus2);
        character.AddStrengthBonus(bonus3);

        Console.WriteLine($"total strength: {character.Strength.Value}");

        Console.WriteLine("\n=== changing basevalue ===");

        character.Strength.BaseValue = 15f;
        Console.WriteLine($"new strength: {character.Strength.Value}");

        Console.WriteLine("\n=== removing a bonus ===");

        character.Strength.RemoveBonus(bonus1);
        Console.WriteLine($"strength after removing bonus: {character.Strength.Value}");

        Console.WriteLine("\n=== testing minvalue and maxvalue ===");

        Attribute testAttribute = new Attribute(50f, 0f, 100f);

        testAttribute.OnValueChanged += (oldValue, newValue) =>
        {
            Console.WriteLine($"test attribute changed: {oldValue} -> {newValue}");
        };

        testAttribute.BaseValue = 150f;
        Console.WriteLine($"value after maximum limit: {testAttribute.Value}");

        testAttribute.BaseValue = -20f;
        Console.WriteLine($"value after minimum limit: {testAttribute.Value}");

        Console.WriteLine("\n=== taking damage ===");

        character.TakeDamage(40f);
        character.TakeDamage(30f);
        character.TakeDamage(30f);
        character.TakeDamage(10f);

        Console.WriteLine("\n=== using stamina ===");

        character.UseStamina(40f);
        character.UseStamina(30f);
        character.UseStamina(30f);
        character.UseStamina(10f);

        Console.WriteLine("\n=== increasing strength ===");

        character.AddStrengthBonus(
            new AttributeBonus(BonusType.Add, 10f));

        character.AddStrengthBonus(
            new AttributeBonus(BonusType.Multiply, 1.5f));

        Console.WriteLine($"final strength: {character.Strength.Value}");

        Console.WriteLine("\n=== program finished ===");
    }
}
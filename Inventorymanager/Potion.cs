public enum PotionType { Healing, Damage }

public class Potion : Item
{
    public PotionType Type { get; set; }
    public int Power { get; set; }

    public Potion(string name, string desc, int level, PotionType type, int power) : base(name, desc)
    {
        Level = level;
        Type = type;
        Power = power;
    }
}
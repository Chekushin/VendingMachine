public class Weapon : Item
{
    public int Damage { get; set; }
    public string? SetId { get; set; }

    public Weapon(string name, string desc, int level, int damage, string? setId = null) : base(name, desc)
    {
        Level = level;
        Damage = damage;
        SetId = setId;
    }
}
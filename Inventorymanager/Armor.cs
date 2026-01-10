public enum ArmorSlot { Head, Chest, Legs }

public class Armor : Item
{
    public int Defense { get; set; }
    public ArmorSlot Slot { get; set; }
    public string? SetId { get; set; }

    public Armor(string name, string desc, int level, int def, ArmorSlot slot, string? setId = null) : base(name, desc)
    {
        Level = level;
        Defense = def;
        Slot = slot;
        SetId = setId;
    }
}
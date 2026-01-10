public class ArmorBuilder
{
    private string name = "Armor";
    private string desc = "";
    private int def = 0;
    private ArmorSlot slot = ArmorSlot.Chest;
    private string? setId = null;

    public ArmorBuilder SetName(string n) { name = n; return this; }
    public ArmorBuilder SetDescription(string d) { desc = d; return this; }
    public ArmorBuilder SetDefense(int d) { def = d; return this; }
    public ArmorBuilder SetSlot(ArmorSlot s) { slot = s; return this; }
    public ArmorBuilder SetSetId(string? s) { setId = string.IsNullOrEmpty(s) ? null : s; return this; }
    public Armor Build() => new Armor(name, desc, 1, def, slot, setId);
}
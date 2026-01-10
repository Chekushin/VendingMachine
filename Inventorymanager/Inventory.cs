using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public List<Item> Items { get; } = new List<Item>();

    public bool AddItem(Item item)
    {
        Items.Add(item);
        return true;
    }

    public bool RemoveItem(string id)
    {
        var item = Items.FirstOrDefault(i => i.Id == id);
        if (item == null || item is QuestItem) return false;
        Items.Remove(item);
        return true;
    }

    public void EquipItem(Item item)
    {
    }

    public void UsePotion(Potion p)
    {
        Items.Remove(p); 
    }

    public void UpgradeItem(Item item)
    {
        item.Level++;
        if (item is Weapon w) w.Damage += 2;
        if (item is Armor a) a.Defense += 2;
        if (item is Potion p) p.Power += 5;
    }

    public int GetSetBonus()
    {
        int bonus = 0;
        var grouped = Items.OfType<Armor>().Where(a => a.SetId != null)
            .GroupBy(a => a.SetId);
        foreach (var g in grouped)
        {
            if (g.Count() >= 2) bonus += 5; 
        }
        return bonus;
    }
}
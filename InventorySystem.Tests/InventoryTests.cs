using System;
using Xunit;
using System.Linq;

public class InventoryTests
{
    [Fact]
    public void AddItem_Succeeds()
    {
        var inv = new Inventory();
        var sword = new Weapon("Sword", "Simple sword", 1, 5);

        var ok = inv.AddItem(sword);

        Assert.True(ok);
        Assert.Contains(sword, inv.Items);
    }

    [Fact]
    public void RemoveItem_Works_QuestItemNotRemoved()
    {
        var inv = new Inventory();
        var potion = new Potion("Health", "Heal", 1, PotionType.Healing, 20);
        var quest = new QuestItem("Quest1", "Important");

        inv.AddItem(potion);
        inv.AddItem(quest);

        var removedPotion = inv.RemoveItem(potion.Id);
        var removedQuest = inv.RemoveItem(quest.Id);

        Assert.True(removedPotion);
        Assert.False(removedQuest); // QuestItem нельзя удалять
        Assert.DoesNotContain(potion, inv.Items);
        Assert.Contains(quest, inv.Items);
    }

    [Fact]
    public void UpgradeItem_IncreasesStats()
    {
        var inv = new Inventory();
        var sword = new Weapon("Sword", "Desc", 1, 5);

        inv.AddItem(sword);
        inv.UpgradeItem(sword);

        Assert.Equal(2, sword.Level);
        Assert.Equal(7, sword.Damage); // +2 после апгрейда
    }

    [Fact]
    public void GetSetBonus_ReturnsCorrectBonus()
    {
        var inv = new Inventory();
        var armor1 = new Armor("Chest", "Desc", 1, 5, ArmorSlot.Chest, "Set1");
        var armor2 = new Armor("Legs", "Desc", 1, 3, ArmorSlot.Legs, "Set1");
        var armor3 = new Armor("Head", "Desc", 1, 2, ArmorSlot.Head, "Set2");

        inv.AddItem(armor1);
        inv.AddItem(armor2);
        inv.AddItem(armor3);

        int bonus = inv.GetSetBonus();

        Assert.Equal(5, bonus); // Только Set1 состоит из 2 предметов => бонус
    }

    [Fact]
    public void UsePotion_RemovesPotion()
    {
        var inv = new Inventory();
        var potion = new Potion("Health", "Heal", 1, PotionType.Healing, 20);

        inv.AddItem(potion);
        inv.UsePotion(potion);

        Assert.DoesNotContain(potion, inv.Items);
    }
}

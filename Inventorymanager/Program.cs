using System;
using System.Linq;
using System.Text;

class Program
{
    static Inventory playerInventory = new Inventory();

    static void Main()
    {   
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
        }
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1 - Добавить предмет");
            Console.WriteLine("2 - Показать инвентарь");
            Console.WriteLine("3 - Показать информацию о предмете");
            Console.WriteLine("4 - Надеть предмет / Использовать зелье");
            Console.WriteLine("5 - Улучшить предмет");
            Console.WriteLine("6 - Удалить предмет (кроме квестовых)");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите действие: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1": AddItemMenu(); break;
                case "2": ShowInventory(); break;
                case "3": ShowItemInfo(); break;
                case "4": EquipOrUseItem(); break;
                case "5": UpgradeItem(); break;
                case "6": RemoveItem(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор"); break;
            }
        }
    }

    static void AddItemMenu()
    {
        Console.WriteLine("Выберите тип предмета: 1-Оружие, 2-Броня, 3-Зелье, 4-Квестовый");
        string? type = Console.ReadLine();

        Console.Write("Имя предмета: ");
        string name = Console.ReadLine() ?? "Безымянный";

        Console.Write("Описание: ");
        string desc = Console.ReadLine() ?? "";

        switch (type)
        {
            case "1":
                Console.Write("Урон: ");
                int dmg = int.TryParse(Console.ReadLine(), out var d) ? d : 0;
                var sword = new Weapon(name, desc, 1, dmg);
                playerInventory.AddItem(sword);
                break;

            case "2":
                Console.Write("Защита: ");
                int def = int.TryParse(Console.ReadLine(), out var df) ? df : 0;
                Console.Write("Слот (Head/Chest/Legs): ");
                string slotStr = Console.ReadLine() ?? "Chest";
                ArmorSlot slot = Enum.TryParse<ArmorSlot>(slotStr, out var s) ? s : ArmorSlot.Chest;
                Console.Write("Сет (Enter если нет): ");
                string? set = Console.ReadLine();
                var armor = new ArmorBuilder()
                                .SetName(name)
                                .SetDescription(desc)
                                .SetDefense(def)
                                .SetSlot(slot)
                                .SetSetId(string.IsNullOrEmpty(set) ? null : set)
                                .Build();
                playerInventory.AddItem(armor);
                break;

            case "3":
                Console.Write("Тип зелья (Healing/Damage): ");
                string typeStr = Console.ReadLine() ?? "Healing";
                PotionType pt = Enum.TryParse<PotionType>(typeStr, out var t) ? t : PotionType.Healing;
                Console.Write("Сила: ");
                int power = int.TryParse(Console.ReadLine(), out var pwr) ? pwr : 0;
                var pot = new Potion(name, desc, 1, pt, power);
                playerInventory.AddItem(pot);
                break;

            case "4":
                var quest = new QuestItem(name, desc);
                playerInventory.AddItem(quest);
                break;

            default:
                Console.WriteLine("Неверный тип");
                break;
        }
    }

    static void ShowInventory()
    {
        if (playerInventory.Items.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст");
            return;
        }

        foreach (var item in playerInventory.Items)
        {
            string extra = "";
            if (item is Armor a)
                extra = $"Слот: {a.Slot}, Защита: {a.Defense}, Сет: {a.SetId}, Уровень: {a.Level}";
            if (item is Weapon w)
                extra = $"Урон: {w.Damage}, Уровень: {w.Level}, Сет: {w.SetId}";
            if (item is Potion p)
                extra = $"Тип: {p.Type}, Сила: {p.Power}, Уровень: {p.Level}";
            if (item is QuestItem q)
                extra = $"Квестовый предмет";

            Console.WriteLine($"{item.Id} - {item.Name} ({item.Description}) {extra}");
        }

        int totalDmg = playerInventory.Items.OfType<Weapon>().Sum(w => w.Damage);
        int totalDef = playerInventory.Items.OfType<Armor>().Sum(a => a.Defense);
        int setBonus = playerInventory.GetSetBonus(); // бонус от сетов
        Console.WriteLine($"\nСуммарно: Урон={totalDmg}, Защита={totalDef}, Бонус от сета={setBonus}");
    }

    static void ShowItemInfo()
    {
        Console.Write("Введите ID предмета: ");
        string? id = Console.ReadLine();
        var item = playerInventory.Items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            Console.WriteLine("Предмет не найден");
            return;
        }
        Console.WriteLine($"ID: {item.Id}, Имя: {item.Name}, Описание: {item.Description}, Уровень: {item.Level}");
        if (item is Armor a) Console.WriteLine($"Защита: {a.Defense}, Слот: {a.Slot}, Сет: {a.SetId}");
        if (item is Weapon w) Console.WriteLine($"Урон: {w.Damage}, Сет: {w.SetId}");
        if (item is Potion p) Console.WriteLine($"Тип: {p.Type}, Сила: {p.Power}");
        if (item is QuestItem) Console.WriteLine("Квестовый предмет");
    }

    static void EquipOrUseItem()
    {
        Console.Write("Введите ID предмета: ");
        string? id = Console.ReadLine();
        var item = playerInventory.Items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            Console.WriteLine("Предмет не найден");
            return;
        }

        if (item is Potion pot)
        {
            playerInventory.UsePotion(pot);
            Console.WriteLine($"{pot.Name} использован");
        }
        else
        {
            playerInventory.EquipItem(item);
            Console.WriteLine($"{item.Name} надет");
        }
    }

    static void UpgradeItem()
    {
        Console.Write("Введите ID предмета: ");
        string? id = Console.ReadLine();
        var item = playerInventory.Items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            Console.WriteLine("Предмет не найден");
            return;
        }
        playerInventory.UpgradeItem(item);
        Console.WriteLine($"{item.Name} улучшен до уровня {item.Level}");
    }

    static void RemoveItem()
    {
        Console.Write("Введите ID предмета для удаления: ");
        string? id = Console.ReadLine();
        var item = playerInventory.Items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            Console.WriteLine("Предмет не найден");
            return;
        }
        if (item is QuestItem)
        {
            Console.WriteLine("Нельзя удалить квестовый предмет");
            return;
        }
        playerInventory.RemoveItem(id!);
        Console.WriteLine($"{item.Name} удален из инвентаря");
    }
}

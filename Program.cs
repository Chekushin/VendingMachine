using System;
using System.Collections.Generic;

class Product
{
    public string Name;
    public int Price;
    public int Count;

    public Product(string n, int p, int c)
    {
        Name = n;
        Price = p;
        Count = c;
    }
}

class VendingMachine
{
    public List<Product> goods = new List<Product>();
    public int balance = 0;
    public int collectedMoney = 0;

    public VendingMachine()
    {
        goods.Add(new Product("Вода", 25, 5));
        goods.Add(new Product("Сок", 40, 3));
        goods.Add(new Product("Чипсы", 60, 4));
        goods.Add(new Product("Шоколад", 80, 2));
        goods.Add(new Product("Кофе", 50, 3));
    }

    public void ShowGoods()
    {
        Console.WriteLine("Список товаров:");
        for (int i = 0; i < goods.Count; i++)
        {
            var g = goods[i];
            Console.WriteLine($"{i + 1}. {g.Name} - {g.Price} руб. (осталось: {g.Count})");
        }
    }

    public void InsertCoin(int value)
    {
        balance += value;
    }

    public bool Buy(int index)
    {
        if (index < 0 || index >= goods.Count)
            return false;
        var g = goods[index];
        if (g.Count <= 0) return false;
        if (balance < g.Price) return false;

        g.Count -= 1;
        balance -= g.Price;
        collectedMoney += g.Price;
        return true;
    }

    public Dictionary<int, int> GiveChange()
    {
        int[] coins = { 100, 50, 20, 10, 5, 2, 1 };
        var res = new Dictionary<int, int>();
        int temp = balance;
        for (int i = 0; i < coins.Length; i++)
        {
            int c = coins[i];
            int cnt = temp / c;
            if (cnt > 0) res[c] = cnt;
            temp = temp % c;
        }
        balance = 0;
        return res;
    }
}

class Program
{
    static VendingMachine vm = new VendingMachine();

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== ВЕНДИНГОВЫЙ АВТОМАТ ===");
            Console.WriteLine($"Баланс: {vm.balance} руб.");
            Console.WriteLine();
            Console.WriteLine("1 - Показать товары");
            Console.WriteLine("2 - Внести монету");
            Console.WriteLine("3 - Купить товар");
            Console.WriteLine("4 - Отмена и возврат денег");
            Console.WriteLine("5 - Администратор");
            Console.WriteLine("0 - Выход");
            Console.Write("Выбор: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    vm.ShowGoods();
                    Pause();
                    break;

                case "2":
                    InsertCoinMenu();
                    break;

                case "3":
                    BuyMenu();
                    break;

                case "4":
                    ReturnChange();
                    break;

                case "5":
                    AdminMenu();
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Неверный выбор!");
                    Pause();
                    break;
            }
        }

        Console.WriteLine("Выход из программы...");
    }

    static void InsertCoinMenu()
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            Console.WriteLine($"Баланс: {vm.balance} руб.");
            Console.WriteLine("1 - 1 руб.");
            Console.WriteLine("2 - 2 руб.");
            Console.WriteLine("3 - 5 руб.");
            Console.WriteLine("4 - 10 руб.");
            Console.WriteLine("5 - 20 руб.");
            Console.WriteLine("6 - 50 руб.");
            Console.WriteLine("7 - 100 руб.");
            Console.WriteLine("0 - Назад");
            Console.Write("Выбор: ");

            string ch = Console.ReadLine();
            switch (ch)
            {
                case "1": vm.InsertCoin(1); break;
                case "2": vm.InsertCoin(2); break;
                case "3": vm.InsertCoin(5); break;
                case "4": vm.InsertCoin(10); break;
                case "5": vm.InsertCoin(20); break;
                case "6": vm.InsertCoin(50); break;
                case "7": vm.InsertCoin(100); break;
                case "0": inMenu = false; break;
                default: Console.WriteLine("Ошибка ввода"); Pause(); break;
            }
        }
    }

    static void BuyMenu()
    {
        Console.Clear();
        vm.ShowGoods();
        Console.WriteLine();
        Console.WriteLine($"Ваш баланс: {vm.balance} руб.");
        Console.Write("Введите номер товара: ");
        string s = Console.ReadLine();
        int num;
        if (int.TryParse(s, out num))
        {
            bool ok = vm.Buy(num - 1);
            if (ok)
            {
                Console.WriteLine("Покупка успешна!");
                var change = vm.GiveChange();
                if (change.Count > 0)
                {
                    Console.WriteLine("Ваша сдача:");
                    foreach (var c in change)
                        Console.WriteLine($"{c.Key} руб. x {c.Value}");
                }
            }
            else Console.WriteLine("Не удалось купить товар!");
        }
        else Console.WriteLine("Некорректный ввод!");
        Pause();
    }

    static void ReturnChange()
    {
        Console.Clear();
        if (vm.balance > 0)
        {
            Console.WriteLine("Возврат монет:");
            var ch = vm.GiveChange();
            foreach (var c in ch)
                Console.WriteLine($"{c.Key} руб. x {c.Value}");
        }
        else Console.WriteLine("Баланс пуст.");
        Pause();
    }

    static void AdminMenu()
    {
        Console.Clear();
        Console.Write("Введите пароль: ");
        string pass = Console.ReadLine();
        if (pass != "1234")
        {
            Console.WriteLine("Неверный пароль!");
            Pause();
            return;
        }

        bool inAdmin = true;
        while (inAdmin)
        {
            Console.Clear();
            Console.WriteLine("=== АДМИНИСТРАТОР ===");
            Console.WriteLine("1 - Показать товары");
            Console.WriteLine("2 - Добавить товар");
            Console.WriteLine("3 - Изменить количество");
            Console.WriteLine("4 - Посмотреть собранные деньги");
            Console.WriteLine("5 - Сбросить собранные деньги");
            Console.WriteLine("0 - Выход");
            Console.Write("Выбор: ");
            string ch = Console.ReadLine();

            switch (ch)
            {
                case "1":
                    vm.ShowGoods();
                    Pause();
                    break;
                case "2":
                    AddProduct();
                    break;
                case "3":
                    ChangeAmount();
                    break;
                case "4":
                    Console.WriteLine($"Собрано: {vm.collectedMoney} руб.");
                    Pause();
                    break;
                case "5":
                    vm.collectedMoney = 0;
                    Console.WriteLine("Деньги сброшены.");
                    Pause();
                    break;
                case "0":
                    inAdmin = false;
                    break;
                default:
                    Console.WriteLine("Ошибка ввода.");
                    Pause();
                    break;
            }
        }
    }

    static void AddProduct()
    {
        Console.Write("Название: ");
        string name = Console.ReadLine();
        Console.Write("Цена: ");
        int price = int.Parse(Console.ReadLine());
        Console.Write("Количество: ");
        int count = int.Parse(Console.ReadLine());
        vm.goods.Add(new Product(name, price, count));
        Console.WriteLine("Товар добавлен.");
        Pause();
    }

    static void ChangeAmount()
    {
        vm.ShowGoods();
        Console.Write("Введите номер товара: ");
        int n = int.Parse(Console.ReadLine());
        if (n > 0 && n <= vm.goods.Count)
        {
            Console.Write("Новое количество: ");
            int c = int.Parse(Console.ReadLine());
            vm.goods[n - 1].Count = c;
            Console.WriteLine("Количество изменено.");
        }
        else Console.WriteLine("Ошибка.");
        Pause();
    }

    static void Pause()
    {
        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using FoodDeliverySystem;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var menu = MenuDatabase.GetInstance();
var factory = new OrderFactory();

Console.WriteLine("=== Система управления заказами ===");

List<EdaItem> cartItems = new List<EdaItem>();
bool isChoosing = true;

while (isChoosing)
{
    Console.WriteLine("\n--- ТЕКУЩАЯ КОРЗИНА ---");
    if (cartItems.Count == 0) Console.WriteLine("(пусто)");
    else
    {
        for (int i = 0; i < cartItems.Count; i++)
            Console.WriteLine($"[{i + 1}] {cartItems[i].Name} ({cartItems[i].Price} руб.)");
    }

    Console.WriteLine("\nДЕЙСТВИЯ:");
    Console.WriteLine("1. Добавить блюдо из меню");
    Console.WriteLine("2. Удалить блюдо из корзины");
    Console.WriteLine("3. Перейти к оформлению заказа");
    Console.WriteLine("0. Выйти из программы");

    Console.Write("\nВыберите действие: ");
    string action = Console.ReadLine();

    switch (action)
    {
        case "1":
            Console.WriteLine("\n--- МЕНЮ ---");
            for (int i = 0; i < menu.items.Count; i++)
                Console.WriteLine($"{i + 1}. {menu.items[i].Name} - {menu.items[i].Price} руб.");
            
            Console.Write("Выберите номер: ");
            if (int.TryParse(Console.ReadLine(), out int mIdx) && mIdx > 0 && mIdx <= menu.items.Count)
                cartItems.Add(menu.items[mIdx - 1]);
            break;

        case "2":
            if (cartItems.Count == 0) break;
            Console.Write("Введите номер блюда для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int rIdx) && rIdx > 0 && rIdx <= cartItems.Count)
                cartItems.RemoveAt(rIdx - 1);
            break;

        case "3":
            if (cartItems.Count > 0) isChoosing = false;
            else Console.WriteLine("Корзина пуста!");
            break;

        case "0":
            return;
    }
}

Console.WriteLine("\n--- ТИП ДОСТАВКИ ---");
Console.WriteLine("1. Стандартная | 2. Быстрая | 3. Персональная (скидка)");
string type = Console.ReadLine();

Order myOrder = type switch
{
    "2" => factory.CreateOrder("fast"),
    "3" => factory.CreateOrder("personal"),
    _ => factory.CreateOrder("normal")
};

foreach (var item in cartItems) myOrder.AddDish(item);

var app = new ClientApp();
myOrder.Attach(app);

Console.WriteLine($"\nИТОГО К ОПЛАТЕ: {myOrder.CalculateTotal()} руб.");
Console.WriteLine("1. Подтвердить заказ | 2. Отменить заказ");
if (Console.ReadLine() == "2")
{
    myOrder.ChangeStatus(OrderStatus.Cancelled);
    Console.WriteLine("Заказ отменен. Возвращайтесь снова!");
    return;
}

myOrder.ChangeStatus(OrderStatus.Preparing);
Thread.Sleep(1500);
myOrder.ChangeStatus(OrderStatus.Delivering);
Thread.Sleep(1500);
myOrder.ChangeStatus(OrderStatus.Completed);

Console.WriteLine("\nНажмите любую клавишу для завершения...");
Console.ReadKey();
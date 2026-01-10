using System;
using System.Collections.Generic;

namespace FoodDeliverySystem
{
    public enum OrderStatus { Created, Preparing, Delivering, Completed, Cancelled }

    public class EdaItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
    
    public class MenuDatabase
    {
        private static MenuDatabase _instance;
        public List<EdaItem> items;

        private MenuDatabase()
        {
            items = new List<EdaItem>
            {
                new EdaItem { Name = "Burger", Price = 100 },
                new EdaItem { Name = "Pizza", Price = 300 },
                new EdaItem { Name = "Sushi", Price = 500 }
            };
        }

        public static MenuDatabase GetInstance()
        {
            if (_instance == null) _instance = new MenuDatabase();
            return _instance;
        }
    }
}
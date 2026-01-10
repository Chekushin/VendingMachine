using Xunit;
using FoodDeliverySystem;
using System.Collections.Generic;

namespace FoodDeliveryTests
{
    public class DeliveryTests
    {
        [Fact]
        public void Test_Menu_Is_Singleton()
        {
            var m1 = MenuDatabase.GetInstance();
            var m2 = MenuDatabase.GetInstance();
            Assert.Same(m1, m2);
        }

        [Theory]
        [InlineData("normal", typeof(StandardOrder))]
        [InlineData("fast", typeof(FastDeliveryOrder))]
        [InlineData("personal", typeof(PersonalOrder))]
        public void Test_Factory_Creates_Correct_Order_Types(string type, System.Type expectedType)
        {
            var factory = new OrderFactory();
            var order = factory.CreateOrder(type);
            Assert.IsType(expectedType, order);
        }

        [Fact]
        public void Test_StandardOrder_Calculation()
        {
            var order = new StandardOrder();
            order.AddDish(new EdaItem { Name = "Test", Price = 100 });
            
            double expected = 270;
            Assert.Equal(expected, order.CalculateTotal());
        }

        [Fact]
        public void Test_FastOrder_Calculation()
        {
            var order = new FastDeliveryOrder();
            order.AddDish(new EdaItem { Name = "Test", Price = 100 });
            
            double expected = 470;
            Assert.Equal(expected, order.CalculateTotal());
        }

        [Fact]
        public void Test_PersonalOrder_Calculation()
        {
            var order = new PersonalOrder(); 
            order.AddDish(new EdaItem { Name = "Test", Price = 100 });
            
            double expected = 108;
            Assert.Equal(expected, order.CalculateTotal());
        }
        
        [Fact]
        public void Test_Remove_Dish_From_Basket()
        {
            var order = new StandardOrder();
            var dish = new EdaItem { Name = "Test", Price = 100 };
            order.AddDish(dish);
            Assert.Single(order.Basket);

            order.RemoveDish(0);
            Assert.Empty(order.Basket);
        }

        [Fact]
        public void Test_Order_Cancellation()
        {
            var order = new StandardOrder();
            var app = new ClientApp();
            order.Attach(app);

            order.ChangeStatus(OrderStatus.Cancelled);
            Assert.Equal("Cancelled", app.LastStatus);
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }
        
        [Fact]
        public void Test_Order_Status_Notification()
        {
            var order = new StandardOrder();
            var clientApp = new ClientApp();
            order.Attach(clientApp);
            
            order.ChangeStatus(OrderStatus.Delivering);
            
            Assert.Equal("Delivering", clientApp.LastStatus);
        }
    }
}
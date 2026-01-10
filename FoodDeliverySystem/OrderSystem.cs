using System.Collections.Generic;

namespace FoodDeliverySystem
{
    public abstract class Order
    {
        public List<EdaItem> Basket = new List<EdaItem>(); 
        public OrderStatus Status = OrderStatus.Created;
        public List<IOrderWatcher> watchers = new List<IOrderWatcher>();
        public IPriceCalcStrategy strategy = new NormalPrice();

        public void AddDish(EdaItem dish) => Basket.Add(dish);

        public void Attach(IOrderWatcher w) => watchers.Add(w);

        public void ChangeStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            foreach (var w in watchers) w.Update(newStatus.ToString());
        }

        public void RemoveDish(int index)
        {
            if (index >= 0 && index < Basket.Count)
            {
                Basket.RemoveAt(index);
            }
        }

        public double CalculateTotal()
        {
            double sum = 0;
            foreach (var item in Basket) sum += strategy.Calculate(item.Price);
            
            OrderCostBase calc = new BaseOrderCost(sum);
            return ApplyDecorators(calc).GetFinalCost();
        }

        protected abstract OrderCostBase ApplyDecorators(OrderCostBase baseCost);
    }

    public class StandardOrder : Order
    {
        protected override OrderCostBase ApplyDecorators(OrderCostBase baseCost) 
            => new DeliveryFeeDecorator(new TaxDecorator(baseCost));
    }

    public class FastDeliveryOrder : Order
    {
        protected override OrderCostBase ApplyDecorators(OrderCostBase baseCost) 
            => new ExpressFeeDecorator(new TaxDecorator(baseCost));
    }

    public class PersonalOrder : Order
    {
        public PersonalOrder() => strategy = new DiscountPrice();
        protected override OrderCostBase ApplyDecorators(OrderCostBase baseCost) 
            => new TaxDecorator(baseCost);
    }

    public class OrderFactory
    {
        public Order CreateOrder(string type)
        {
            return type.ToLower() switch
            {
                "fast" => new FastDeliveryOrder(),
                "personal" => new PersonalOrder(),
                _ => new StandardOrder()
            };
        }
    }
}
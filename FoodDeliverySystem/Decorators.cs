namespace FoodDeliverySystem
{
    // PATTERN: Decorator
    public abstract class OrderCostBase
    {
        public abstract double GetFinalCost();
    }

    public class BaseOrderCost : OrderCostBase
    {
        private double _price;
        public BaseOrderCost(double price) => _price = price;
        public override double GetFinalCost() => _price;
    }

    public class TaxDecorator : OrderCostBase
    {
        private OrderCostBase _prev;
        public TaxDecorator(OrderCostBase prev) => _prev = prev;
        public override double GetFinalCost() => _prev.GetFinalCost() * 1.2; 
    }

    public class DeliveryFeeDecorator : OrderCostBase
    {
        private OrderCostBase _prev;
        public DeliveryFeeDecorator(OrderCostBase prev) => _prev = prev;
        public override double GetFinalCost() => _prev.GetFinalCost() + 150; 
    }

    public class ExpressFeeDecorator : OrderCostBase
    {
        private OrderCostBase _prev;
        public ExpressFeeDecorator(OrderCostBase prev) => _prev = prev;
        public override double GetFinalCost() => _prev.GetFinalCost() + 350; 
    }
}
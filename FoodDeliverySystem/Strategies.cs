namespace FoodDeliverySystem
{
    public interface IPriceCalcStrategy
    {
        double Calculate(double basePrice);
    }

    public class NormalPrice : IPriceCalcStrategy
    {
        public double Calculate(double basePrice) => basePrice;
    }

    public class DiscountPrice : IPriceCalcStrategy
    {
        public double Calculate(double basePrice) => basePrice * 0.9;
    }
}
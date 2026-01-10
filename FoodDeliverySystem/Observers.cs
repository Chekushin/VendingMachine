namespace FoodDeliverySystem
{
    public interface IOrderWatcher
    {
        void Update(string status);
    }

    public class ClientApp : IOrderWatcher
    {
        public string LastStatus { get; private set; }
        public void Update(string status)
        {
            LastStatus = status;
            Console.WriteLine($"[Уведомление клиенту]: Статус заказа изменился на -> {status}");
        }
    }
}
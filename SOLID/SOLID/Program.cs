public class OrderService
{
    private readonly IOrderValidation _orderValidation;
    private readonly IPaymentProcessor _paymentProcessing;
    private readonly INotification _notification;
    private readonly IOrderPersistence _orderPersistence;

    public OrderService(IOrderValidation orderValidartion, IPaymentProcessor paymentProcessing, INotification notification, IOrderPersistence orderPersistence)
    {
        _orderValidation = orderValidartion;
        _paymentProcessing = paymentProcessing;
        _notification = notification;
        _orderPersistence = orderPersistence;
    }

    public void ProcessOrder(Order order)
    {
        // Validation
        _orderValidation.ValidateOrder(order);

        // Payment
        _paymentProcessing.ProcessPayment(order.PaymentMethod);

        // Notification
        _notification.SendEmailNotification(order.CustomerEmail);

        // Persistence
        _orderPersistence.SaveOrderId(order.Id);
    }
}

public interface IOrderValidation
{
    void ValidateOrder(Order order);
}

public class OrderValidation : IOrderValidation
{
    public void ValidateOrder(Order order)
    {
        if (order == null)
            throw new Exception("Order is null");

        if (order.Total <= 0)
            throw new Exception("Invalid total");
    }
}

public interface IPaymentProcessor
{
    void ProcessPayment(string paymentMethod);
}

public class PaymentProcessing : IPaymentProcessor
{
    public void ProcessPayment(string paymentMethod)
    {
        if (paymentMethod == "CreditCard")
        {
            Console.WriteLine("Paid with credit card");
        }
        else if (paymentMethod == "PayPal")
        {
            Console.WriteLine("Paid with PayPal");
        }
        else
        {
            throw new Exception("Unknown payment method");
        }
    }
}

public interface INotification
{
    void SendEmailNotification(string email);
}

public class Notification : INotification
{
    public void SendEmailNotification(string email)
    {
        if (email != null)
        {
            Console.WriteLine($"Email sent to {email}");
        }
    }
}

public interface IOrderPersistence
{
    void SaveOrderId(int id);
}

public class OrderPersistence : IOrderPersistence
{
    public void SaveOrderId(int id)
    {
        File.AppendAllText("orders.txt", id + Environment.NewLine);
    }
}

public class Order
{
    public int Id { get; set; }
    public decimal Total { get; set; }
    public string PaymentMethod { get; set; }
    public string? CustomerEmail { get; set; }
}

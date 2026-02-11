public class OrderService
{
    private readonly IOrderValidation _orderValidation;
    private readonly IEnumerable<IPaymentProcessor> _paymentMethods;
    private readonly INotification _notification;
    private readonly IOrderPersistence _orderPersistence;

    public OrderService(IOrderValidation orderValidartion, IEnumerable<IPaymentProcessor> paymentMethods, INotification notification, IOrderPersistence orderPersistence)
    {
        _orderValidation = orderValidartion;
        _paymentMethods = paymentMethods;
        _notification = notification;
        _orderPersistence = orderPersistence;
    }

    public void ProcessOrder(Order order)
    {
        // Validation
        _orderValidation.ValidateOrder(order);

        // Payment
        var paymentType = _paymentMethods.FirstOrDefault(p => p.CanProcessPayment(order.PaymentMethod));

        if (paymentType == null)
        {
            throw new Exception("Payment method is not supported.");
        }

        paymentType.ProcessPayment(order.Total);

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
    bool CanProcessPayment(string paymentMethod);

    void ProcessPayment(decimal total);
}

public class CreditCardPayment : IPaymentProcessor
{
    public bool CanProcessPayment(string paymentMethod)
    {
        return paymentMethod == "CreditCard";
    }

    public void ProcessPayment(decimal total)
    {
        Console.WriteLine($"Paid {total} with credit card");
    }
}

public class PaypalPayment : IPaymentProcessor
{
    public bool CanProcessPayment(string paymentMethod)
    {
        return paymentMethod == "Paypal";
    }

    public void ProcessPayment(decimal total)
    {
        Console.WriteLine($"Paid {total} with PayPal");
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

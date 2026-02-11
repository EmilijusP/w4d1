public interface IOrderService
{ 
    void ProcessOrder(); 

    void SendEmail(); 

    void SaveToFile(); 

}

public class OrderService
{
    private readonly IOrderValidation _orderValidation;
    private readonly IEnumerable<IPaymentProcessor> _paymentMethods;
    private readonly IEmailNotification _emailNotification;
    private readonly IOrderPersistence _orderPersistence;

    public OrderService(IOrderValidation orderValidartion, IEnumerable<IPaymentProcessor> paymentMethods, IEmailNotification emailNotification, IOrderPersistence orderPersistence)
    {
        _orderValidation = orderValidartion;
        _paymentMethods = paymentMethods;
        _emailNotification = emailNotification;
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
        if (_emailNotification.IsValidEmail(order.CustomerEmail))
        {
            _emailNotification.SendNotification(order.CustomerEmail);
        }

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

public interface IEmailNotification
{
    bool IsValidEmail(string email);

    void SendNotification(string email);
}

public class EmailNotification : IEmailNotification
{
    public bool IsValidEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            return true;
        }

        return false;
    }

    public void SendNotification(string email)
    {
        Console.WriteLine($"Email sent to {email}");
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

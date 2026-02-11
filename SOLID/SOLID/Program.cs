using System.ComponentModel.DataAnnotations;

public class OrderService
{
    private readonly ILogger _logger;
    private readonly IOrderValidation _orderValidation;
    private readonly IEnumerable<IPaymentProcessor> _paymentMethods;
    private readonly IEmailNotification _emailNotification;
    private readonly IOrderRepository _orderRepository;

    public OrderService(ILogger logger, IOrderValidation orderValidation, IEnumerable<IPaymentProcessor> paymentMethods, IEmailNotification emailNotification, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderValidation = orderValidation;
        _paymentMethods = paymentMethods;
        _emailNotification = emailNotification;
        _orderRepository = orderRepository;
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
        _orderRepository.SaveOrder(order);
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
    private readonly ILogger _logger;

    public CreditCardPayment(ILogger logger)
    {
        _logger = logger;
    }

    public bool CanProcessPayment(string paymentMethod)
    {
        return paymentMethod == "CreditCard";
    }

    public void ProcessPayment(decimal total)
    {
        var message = $"Paid {total} with credit card";
        _logger.Log(message);
    }
}

public class PaypalPayment : IPaymentProcessor
{
    private readonly ILogger _logger;

    public PaypalPayment(ILogger logger)
    {
        _logger = logger;
    }

    public bool CanProcessPayment(string paymentMethod)
    {
        return paymentMethod == "Paypal";
    }

    public void ProcessPayment(decimal total)
    {
        var message = $"Paid {total} with PayPal";
        _logger.Log(message);
    }
}

public interface IEmailNotification
{
    bool IsValidEmail(string email);

    void SendNotification(string email);
}

public class EmailNotification : IEmailNotification
{
    private readonly ILogger _logger;

    public EmailNotification(ILogger logger)
    {
        _logger = logger;
    }

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
        var message = $"Email sent to {email}";
        _logger.Log(message);
    }
}

public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

public interface IOrderRepository
{
    void SaveOrder(Order order);
}

public class FileOrderRepository : IOrderRepository
{
    public void SaveOrder(Order order)
    {
        File.AppendAllText("orders.txt", $"{order.Id}\t{order.Total}\t{order.PaymentMethod}\t{order.CustomerEmail}" + Environment.NewLine);
    }
}

public class Order
{
    public int Id { get; set; }
    public decimal Total { get; set; }
    public string PaymentMethod { get; set; }
    public string? CustomerEmail { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var logger = new ConsoleLogger();

        var validator = new OrderValidation();

        var paymentMethods = new List<IPaymentProcessor>
        {
            new CreditCardPayment(logger),
            new PaypalPayment(logger)
        };

        var emailNotification = new EmailNotification(logger);

        var orderRepository = new FileOrderRepository();

        var orderService = new OrderService(logger, validator, paymentMethods, emailNotification, orderRepository);

        var myOrder = new Order
        {
            Id = 1,
            Total = 45,
            PaymentMethod = "Paypal",
            CustomerEmail = "customer@mail.com"
        };

        try
        {
            orderService.ProcessOrder(myOrder);
            Console.WriteLine("Order processed");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

    }
}
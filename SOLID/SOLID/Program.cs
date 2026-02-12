using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using SOLID.Interfaces;
using SOLID.Classes;
using SOLID.Models;

    var logger = new ConsoleLogger();

    var validator = new OrderValidation();

    var paymentMethods = new List<IPaymentProcessor>
    {
        new CreditCardPayment(logger),
        new PaypalPayment(logger),
        new GooglePayPayment(logger)
    };

    var emailNotification = new EmailNotification(logger);

    var orderRepository = new FileOrderRepository();

    var orderService = new OrderService(logger, validator, paymentMethods, emailNotification, orderRepository);

    var myOrder = new Order
    {
        Id = 1,
        Total = 50,
        PaymentMethod = "GooglePay",
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
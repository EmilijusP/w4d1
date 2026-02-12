using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using SOLID.BusinessLogic;
using SOLID.Contracts.Models;

    var logger = new ConsoleLogger();

    var validator = new OrderValidation();

    var paymentMethod = new BankTransferPayment(logger);

    var emailNotification = new EmailNotification(logger);

    var orderRepository = new FileOrderRepository();

    var orderService = new OrderService(logger, validator, paymentMethod, emailNotification, orderRepository);

    var myOrder = new Order
    {
        Id = 1,
        Total = 50,
        PaymentMethod = "BankTransfer",
        CustomerEmail = "customer@mail.com"
    };

    orderService.ProcessOrder(myOrder);
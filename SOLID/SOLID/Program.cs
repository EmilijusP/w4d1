using SOLID.BusinessLogic;
using SOLID.BusinessLogic.Decorators;
using SOLID.BusinessLogic.Facades;
using SOLID.Contracts.Interfaces;
using SOLID.Contracts.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

var myOrder = new Order
{
    Id = 1,
    Total = 50,
    PaymentMethod = "BankTransfer",
    CustomerEmail = "customer@mail.com"
};

ILogger logger = new ConsoleLogger();

IOrderValidation validator = new OrderValidation();

IPaymentStrategy paymentMethod = new PaymentLoggingDecorator(new PaymentTimingDecorator(new BankTransferPayment(logger), logger), logger);

IOrderRepository orderRepository = new FileOrderRepository();

OrderEventPublisher orderEventPublisher = new OrderEventPublisher();
IOrderObserver emailNotifier = new EmailNotifier(logger);
IOrderObserver auditLogger = new AuditLogger(logger);
orderEventPublisher.Subscribe(emailNotifier);
orderEventPublisher.Subscribe(auditLogger);

IOrderService orderService = new OrderService(logger, validator, paymentMethod, orderRepository, orderEventPublisher);

OrderFacade orderFacade = new OrderFacade(orderService);

orderFacade.ProcessNewOrder(myOrder);
using SOLID.Contracts.Interfaces;
using SOLID.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{

    public class OrderService
    {
        private readonly ILogger _logger;
        private readonly IOrderValidation _orderValidation;
        private readonly IPaymentStrategy _paymentMethod;
        private readonly IEmailNotification _emailNotification;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ILogger logger, IOrderValidation orderValidation, IPaymentStrategy paymentMethod, IEmailNotification emailNotification, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderValidation = orderValidation;
            _paymentMethod = paymentMethod;
            _emailNotification = emailNotification;
            _orderRepository = orderRepository;
        }

        public void ProcessOrder(Order order)
        {
            // Validation
            _orderValidation.ValidateOrder(order);

            // Payment
            _paymentMethod.Pay(order.Total);

            // Notification
            _emailNotification.SendNotification(order.CustomerEmail);

            // Persistence
            _orderRepository.SaveOrder(order);

            // 
            _logger.Log("Order processed.");
        }
    }
}

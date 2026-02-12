using SOLID.Interfaces;
using SOLID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Classes
{

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
            var paymentType = _paymentMethods.FirstOrDefault(p => p.CanProcessPayment(order.PaymentMethod, order.Total));

            if (paymentType == null)
            {
                _logger.ThrowException("Payment method is not supported.");
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
}

using SOLID.Contracts.Interfaces;
using SOLID.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{

    public class OrderService : IOrderService
    {
        private readonly ILogger _logger;
        private readonly IOrderValidation _orderValidation;
        private readonly IPaymentStrategy _paymentMethod;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderEventPublisher _orderEventPublisher;

        public OrderService(ILogger logger, IOrderValidation orderValidation, IPaymentStrategy paymentMethod, IOrderRepository orderRepository, OrderEventPublisher orderEventPublisher)
        {
            _logger = logger;
            _orderValidation = orderValidation;
            _paymentMethod = paymentMethod;
            _orderRepository = orderRepository;
            _orderEventPublisher = orderEventPublisher;
        }

        public void ProcessOrder(Order order)
        {
            _orderValidation.ValidateOrder(order);

            _paymentMethod.Pay(order.Total);

            _orderRepository.SaveOrder(order);

            _orderEventPublisher.Notify(order);

            _logger.Log("Order processed.");
        }
    }
}

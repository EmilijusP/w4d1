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
        private readonly IPaymentStrategyFactory _paymentStrategyFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderEventPublisher _orderEventPublisher;
        private readonly IPaymentPipelineFactory _paymentPipelineFactory;

        public OrderService(ILogger logger, IOrderValidation orderValidation, IPaymentStrategyFactory paymentStrategyFactory, IOrderRepository orderRepository, IOrderEventPublisher orderEventPublisher, IPaymentPipelineFactory paymentPipelineFactory)
        {
            _logger = logger;
            _orderValidation = orderValidation;
            _paymentStrategyFactory = paymentStrategyFactory;
            _orderRepository = orderRepository;
            _orderEventPublisher = orderEventPublisher;
            _paymentPipelineFactory = paymentPipelineFactory;
        }

        public async void ProcessOrder(Order order)
        {
            _orderValidation.ValidateOrder(order);

            var paymentStrategy = _paymentStrategyFactory.SelectPaymentStrategy(order);

            var paymentPipeline = _paymentPipelineFactory.CreatePipeline(paymentStrategy);

            await paymentPipeline.Execute(order.Total);

            _orderRepository.SaveOrder(order);

            _orderEventPublisher.Notify(order);

            _logger.Log("Order processed.");
        }
    }
}

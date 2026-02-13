using SOLID.BusinessLogic.Decorators;
using SOLID.Contracts.Interfaces;
using SOLID.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class PaymentStrategyFactory : IPaymentStrategyFactory
    {
        private readonly ILogger _logger;

        public PaymentStrategyFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IPaymentStrategy SelectPaymentStrategy(Order order)
        {
            switch (order.PaymentMethod)
            {
                case "CreditCard":
                    return new PaymentLoggingDecorator(new PaymentTimingDecorator(new CreditCardPayment(_logger), _logger), _logger);

                case "Paypal":
                    return new PaymentLoggingDecorator(new PaymentTimingDecorator(new PaypalPayment(_logger), _logger), _logger);

                case "BankTransfer":
                    return new PaymentLoggingDecorator(new PaymentTimingDecorator(new BankTransferPayment(_logger), _logger), _logger);

                default:
                    throw new ArgumentException($"{order.PaymentMethod} is not available as a payment method.");
            }
        }
    }
}

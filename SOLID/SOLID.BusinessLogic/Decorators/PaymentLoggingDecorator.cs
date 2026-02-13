using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic.Decorators
{
    public class PaymentLoggingDecorator : IPaymentStrategy
    {
        private readonly IPaymentStrategy _decoratedPayment;
        private readonly ILogger _logger;

        public PaymentLoggingDecorator(IPaymentStrategy decoratedPayment, ILogger logger)
        {
            _decoratedPayment = decoratedPayment;
            _logger = logger;
        }

        public void Pay(decimal total)
        {
            if (AppSettings.Instance.EnablePaymentLogging)
            {
                _logger.Log("Starting payment.");
                _decoratedPayment.Pay(total);
                _logger.Log("Payment finished.");
            }

            else
            {
                _decoratedPayment.Pay(total);
            }
        }
    }
}

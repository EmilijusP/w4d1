using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class PaymentExecutionStep : IPaymentStep
    {
        private readonly ILogger _logger;
        private readonly IPaymentStrategy _paymentStrategy;

        public PaymentExecutionStep(ILogger logger, IPaymentStrategy paymentStrategy)
        {
            _logger = logger;
            _paymentStrategy = paymentStrategy;
        }

        public async Task Handle(decimal total, Func<Task> next)
        {
            _paymentStrategy.Pay(total);    

            await next();
        }
    }
}

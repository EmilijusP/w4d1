using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class PaypalPayment : IPaymentProcessor
    {
        private readonly ILogger _logger;

        public PaypalPayment(ILogger logger)
        {
            _logger = logger;
        }

        public void ProcessPayment(decimal total)
        {
            var message = $"Paid {total} with PayPal";
            _logger.Log(message);
        }
    }
}

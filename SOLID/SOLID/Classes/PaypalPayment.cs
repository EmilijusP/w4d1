using SOLID.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Classes
{
    public class PaypalPayment : IPaymentProcessor
    {
        private readonly ILogger _logger;

        public PaypalPayment(ILogger logger)
        {
            _logger = logger;
        }

        public bool CanProcessPayment(string paymentMethod, decimal total)
        {
            return paymentMethod == "Paypal";
        }

        public void ProcessPayment(decimal total)
        {
            var message = $"Paid {total} with PayPal";
            _logger.Log(message);
        }
    }
}

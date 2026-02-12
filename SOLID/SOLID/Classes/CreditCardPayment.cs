using SOLID.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Classes
{
    public class CreditCardPayment : IPaymentProcessor
    {
        private readonly ILogger _logger;

        public CreditCardPayment(ILogger logger)
        {
            _logger = logger;
        }

        public bool CanProcessPayment(string paymentMethod, decimal total)
        {
            return paymentMethod == "CreditCard";
        }

        public void ProcessPayment(decimal total)
        {
            var message = $"Paid {total} with credit card";
            _logger.Log(message);
        }
    }
}

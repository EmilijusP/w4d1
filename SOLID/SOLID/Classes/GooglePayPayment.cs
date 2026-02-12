using SOLID.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Classes
{
    public class GooglePayPayment : IPaymentProcessor
    {
        private readonly ILogger _logger;

        public GooglePayPayment(ILogger logger)
        {
            _logger = logger;
        }

        public bool CanProcessPayment(string paymentMethod, decimal total)
        {
            return paymentMethod == "GooglePay" && total <= 100;
        }

        public void ProcessPayment(decimal total)
        {
            var message = $"Paid {total} with GooglePay";
            _logger.Log(message);
        }
    }
}

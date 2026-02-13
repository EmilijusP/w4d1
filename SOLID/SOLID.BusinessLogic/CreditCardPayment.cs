using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class CreditCardPayment : IPaymentStrategy
    {
        private readonly ILogger _logger;

        public CreditCardPayment(ILogger logger)
        {
            _logger = logger;
        }

        public void Pay(decimal total)
        {
            _logger.Log($"Paid {total} with CreditCard");
        }
    }
}

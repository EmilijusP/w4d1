using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class PaymentAuditStep : IPaymentStep
    {
        private readonly ILogger _logger;

        public PaymentAuditStep(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(decimal total, Func<Task> next)
        {
            _logger.Log("Payment has been audited.");

            await next();
        }
    }
}

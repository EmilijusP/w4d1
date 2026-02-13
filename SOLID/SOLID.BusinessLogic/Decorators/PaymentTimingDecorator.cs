using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic.Decorators
{
    public class PaymentTimingDecorator : IPaymentStrategy
    {
        private readonly IPaymentStrategy _decoratedPayment;
        private readonly ILogger _logger;

        public PaymentTimingDecorator(IPaymentStrategy decoratedPayment, ILogger logger)
        {
            _decoratedPayment = decoratedPayment;
            _logger = logger;
        }

        public void Pay(decimal total)
        {
            if (AppSettings.Instance.EnablePaymentTiming)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                _decoratedPayment.Pay(total);
                stopwatch.Stop();
                _logger.Log($"Payment was processed in {stopwatch.ElapsedMilliseconds} ms.");
            }
            
            else
            {
                _decoratedPayment.Pay(total);
            }
        }

    }
}

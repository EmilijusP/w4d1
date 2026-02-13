using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class PaymentPipeline : IPaymentPipeline
    {
        private List<IPaymentStep> _paymentSteps;

        public PaymentPipeline()
        {
            _paymentSteps = new List<IPaymentStep>();
        }

        public void AddStep(IPaymentStep paymentStep)
        {
            _paymentSteps.Add(paymentStep);
        }

        public async Task Execute(decimal total)
        {
            Func<Task> next = () => Task.CompletedTask;

            for (int i = _paymentSteps.Count - 1; i >= 0; i--)
            {
                IPaymentStep paymentStep = _paymentSteps[i];

                var currentNext = next;

                next = () => paymentStep.Handle(total, currentNext);
            }

            await next();
        }
    }
}

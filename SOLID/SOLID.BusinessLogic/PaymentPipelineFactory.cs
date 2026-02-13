using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class PaymentPipelineFactory : IPaymentPipelineFactory
    {
        private readonly ILogger _logger;

        public PaymentPipelineFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IPaymentPipeline CreatePipeline(IPaymentStrategy paymentStrategy)
        {
            var pipeline = new PaymentPipeline();

            pipeline.AddStep(new PaymentValidationStep(_logger, paymentStrategy));
            pipeline.AddStep(new PaymentExecutionStep(_logger, paymentStrategy));
            pipeline.AddStep(new PaymentAuditStep(_logger));

            return pipeline;
        }
    }
}

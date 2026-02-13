using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Contracts.Interfaces
{
    public interface IPaymentPipeline
    {
        void AddStep(IPaymentStep paymentStep);

        Task Execute(decimal total);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Contracts.Interfaces
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(decimal total);
    }
}

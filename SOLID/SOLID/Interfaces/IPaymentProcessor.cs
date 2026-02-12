using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Interfaces
{
    public interface IPaymentProcessor
    {
        bool CanProcessPayment(string paymentMethod, decimal total);

        void ProcessPayment(decimal total);
    }
}

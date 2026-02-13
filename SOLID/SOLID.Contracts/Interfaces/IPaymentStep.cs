using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Contracts.Interfaces
{
    public interface IPaymentStep
    {
        Task Handle(decimal amount, Func<Task> next);
    }
}

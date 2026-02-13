using SOLID.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Contracts.Interfaces
{
    public interface IOrderEventPublisher
    {
        void Subscribe(IOrderObserver observer);

        void Notify(Order order);
    }
}

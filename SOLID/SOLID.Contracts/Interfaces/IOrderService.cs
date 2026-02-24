using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Contracts.Models;

namespace SOLID.Contracts.Interfaces
{
    public interface IOrderService
    {
        Task ProcessOrder(Order order);
    }
}

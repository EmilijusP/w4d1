using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.Models;

namespace SOLID.Interfaces
{
    public interface IOrderValidation
    {
        void ValidateOrder(Order order);
    }
}

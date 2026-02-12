using SOLID.Interfaces;
using SOLID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Classes
{
    public class OrderValidation : IOrderValidation
    {
        public void ValidateOrder(Order order)
        {
            if (order == null)
                throw new Exception("Order is null");

            if (order.Total <= 0)
                throw new Exception("Invalid total");
        }
    }
}

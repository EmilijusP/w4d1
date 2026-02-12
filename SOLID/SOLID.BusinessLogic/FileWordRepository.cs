using SOLID.Contracts.Interfaces;
using SOLID.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class FileOrderRepository : IOrderRepository
    {
        public void SaveOrder(Order order)
        {
            File.AppendAllText("orders.txt", $"{order.Id}\t{order.Total}\t{order.PaymentMethod}\t{order.CustomerEmail}" + Environment.NewLine);
        }
    }
}

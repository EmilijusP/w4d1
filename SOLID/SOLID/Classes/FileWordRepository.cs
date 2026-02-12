using SOLID.Interfaces;
using SOLID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Classes
{
    public class FileOrderRepository : IOrderRepository
    {
        public void SaveOrder(Order order)
        {
            File.AppendAllText("orders.txt", $"{order.Id}\t{order.Total}\t{order.PaymentMethod}\t{order.CustomerEmail}" + Environment.NewLine);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Contracts.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string? CustomerEmail { get; set; }
    }
}

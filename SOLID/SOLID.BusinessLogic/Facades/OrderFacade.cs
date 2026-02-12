using SOLID.BusinessLogic.Decorators;
using SOLID.Contracts.Models;
using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic.Facades
{
    public class OrderFacade
    {
        private readonly IOrderService _orderService;

        public OrderFacade(IOrderService orderService)
        {
            _orderService = orderService;
        }


        public void ProcessNewOrder(Order order)
        {
            _orderService.ProcessOrder(order);
        }
    }
}

using SOLID.Contracts.Interfaces;
using SOLID.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class OrderEventPublisher
    {
        private readonly List<IOrderObserver> _orderObservers;

        public OrderEventPublisher()
        {
            _orderObservers = new List<IOrderObserver>();
        }
        
        public void Subscribe(IOrderObserver observer)
        {
            if (observer != null && !_orderObservers.Contains(observer))
            {
                _orderObservers.Add(observer);
            }
        }

        public void Notify(Order order)
        {
            foreach (var observer in _orderObservers)
            {
                observer.Update(order);
            }
        }
    }
}

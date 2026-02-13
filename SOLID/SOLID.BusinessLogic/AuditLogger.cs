using SOLID.Contracts.Interfaces;
using SOLID.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class AuditLogger : IOrderObserver
    {
        private readonly ILogger _logger;

        public AuditLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Update(Order order)
        {
            LogAudit(order.Id);
        }

        private void LogAudit(int id)
        {
            var message = $"Order with ID {id} has been logged.";
            _logger.Log(message);
        }
    }
}

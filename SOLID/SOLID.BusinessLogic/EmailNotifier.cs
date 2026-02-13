using SOLID.Contracts.Interfaces;
using SOLID.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class EmailNotifier : IOrderObserver
    {
        private readonly ILogger _logger;

        public EmailNotifier(ILogger logger)
        {
            _logger = logger;
        }

        public void Update(Order order)
        {
            SendEmail(order.CustomerEmail);
        }
        
        private void SendEmail(string email)
        {
            if (IsValidEmail(email))
            {
                var message = $"Email sent to {email}";
                _logger.Log(message);
            }
        }

        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email);
        }
    }
}

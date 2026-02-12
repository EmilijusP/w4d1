using SOLID.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Classes
{
    public class EmailNotification : IEmailNotification
    {
        private readonly ILogger _logger;

        public EmailNotification(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsValidEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return true;
            }

            return false;
        }

        public void SendNotification(string email)
        {
            var message = $"Email sent to {email}";
            _logger.Log(message);
        }
    }
}

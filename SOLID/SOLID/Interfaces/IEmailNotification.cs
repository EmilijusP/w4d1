using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Interfaces
{
    public interface IEmailNotification
    {
        bool IsValidEmail(string email);

        void SendNotification(string email);
    }
}

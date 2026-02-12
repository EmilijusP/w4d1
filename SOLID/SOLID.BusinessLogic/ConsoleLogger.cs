using SOLID.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            if (AppSettings.Instance.Environment == "Staging")
            {
                Console.WriteLine(message);
            }
        }
    }
}

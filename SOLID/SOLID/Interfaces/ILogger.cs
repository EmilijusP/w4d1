using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Interfaces
{
    public interface ILogger
    {
        void Log(string message);

        void ThrowException(string meesage);
    }
}

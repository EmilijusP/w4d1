using SOLID.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Contracts.Interfaces
{
    public interface IOrderObserver
    {
        void Update(Order order);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IMemoryCache<T>
    {
        void Add(string key, T value);

        bool TryGet(string key, out T value);

    }
}

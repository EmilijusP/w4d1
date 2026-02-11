using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Models
{
    public class MemoryCache<T> : IMemoryCache<T>
    {
        private Dictionary<string, T> _cache = new();

        public void Add(string key, T value)
        {
            _cache.Add(key, value);
        }

        public bool TryGet(string key, out T? value)
        {
            //value = default;

            //if (!string.IsNullOrEmpty(key))
            //{
            //    return false;
            //}

            //if (_cache.TryGetValue(key, out var entries))
            //{
            //    value = entries;
            //    return true;
            //}

            //return false;

            return _cache.TryGetValue(key, out value);
        }
    }
}

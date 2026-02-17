using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool CanFitWithin(this Dictionary<char, int> source, Dictionary<char, int> target)
        {
            if (source == null || target == null)
            {
                return false;
            }

            foreach (var pair in source)
                if (!target.ContainsKey(pair.Key) || pair.Value > target[pair.Key])
                    return false;

            return true;
        }
    }
}

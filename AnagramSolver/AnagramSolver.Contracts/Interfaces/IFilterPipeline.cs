using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IFilterPipeline
    {
        IEnumerable<Anagram> Execute(IEnumerable<Anagram> anagrams, int minOutputWordLength, Dictionary<char, int> inputCharCount);
    }
}

using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISimpleAnagramAlgorithm
    {
        IList<string> GetAnagrams(Dictionary<char, int> targetLetters, int anagramCount, List<Anagram> allAnagrams, int minOutputWordsLength);
    }
}

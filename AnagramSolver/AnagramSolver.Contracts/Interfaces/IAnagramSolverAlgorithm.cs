using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramSolverAlgorithm
    {
        List<string> GetAnagrams(
            Dictionary<char, int> targetLetters,
            int maxWordsAmount,
            List<Anagram> allAnagrams,
            int minOutputWordLength);
    }
}

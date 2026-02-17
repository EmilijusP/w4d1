using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IComplexAnagramAlgorithm
    {

        List<List<string>> FindKeyCombinations(Dictionary<char, int> targetLetters, int maxWordsAmount, List<Anagram> anagramModels);

        List<string> CreateCombinations(List<List<string>> keyCombinations, List<Anagram> possibleAnagrams);

    }
}

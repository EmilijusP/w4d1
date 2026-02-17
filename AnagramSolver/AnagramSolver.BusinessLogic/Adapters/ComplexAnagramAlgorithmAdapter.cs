using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Adapters
{
    public class ComplexAnagramAlgorithmAdapter : IAnagramSolverAlgorithm
    {
        private readonly IComplexAnagramAlgorithm _complexAnagramAlgorithm;

        public ComplexAnagramAlgorithmAdapter(IComplexAnagramAlgorithm complexAnagramAlgorithm)
        {
            _complexAnagramAlgorithm = complexAnagramAlgorithm;
        }

        public IList<string> GetAnagrams(Dictionary<char, int> targetLetters, int anagramCount, List<Anagram> allAnagrams, int minOutputWordsLength)
        {

            var keyCombinations = _complexAnagramAlgorithm.FindKeyCombinations(targetLetters, anagramCount, allAnagrams);

            var anagramList = _complexAnagramAlgorithm.CreateCombinations(keyCombinations, allAnagrams);

            return anagramList;
        }
    }
}

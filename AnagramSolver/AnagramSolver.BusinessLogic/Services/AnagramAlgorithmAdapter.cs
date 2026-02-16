using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramAlgorithmAdapter : IAnagramSolverAlgorithm
    {
        private readonly IComplexAnagramAlgorithm _inner;

        public AnagramAlgorithmAdapter(IComplexAnagramAlgorithm inner)
        {
            _inner = inner;
        }

        public List<string> GetAnagrams(
            Dictionary<char, int> targetLetters,
            int maxWordsAmount,
            List<Anagram> allAnagrams,
            int minOutputWordLength)
        {
            // reuse existing complex algorithm: first filter candidates then compute
            var filteredByLength = allAnagrams.Where(a => _inner.IsValidOutputLength(a.Key, minOutputWordLength));
            var possibleAnagrams = filteredByLength.Where(a => _inner.CanFitWithin(a.KeyCharCount, targetLetters)).ToList();

            var keyCombinations = _inner.FindKeyCombinations(targetLetters, maxWordsAmount, possibleAnagrams);
            var results = _inner.CreateCombinations(keyCombinations, possibleAnagrams);

            // final lightweight filter: ensure every word in the sentence meets min length
            return results.Where(sentence => sentence.Split(' ').All(w => w.Length >= minOutputWordLength)).ToList();
        }
    }
}

using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class SimpleAnagramAlgorithm : ISimpleAnagramAlgorithm
    {
        private readonly IWordProcessor _wordProcessor;

        public SimpleAnagramAlgorithm(IWordProcessor wordProcessor)
        {
            _wordProcessor = wordProcessor;
        }

        public IList<string> GetAnagrams(Dictionary<char, int> targetLetters, int anagramCount, List<Anagram> allAnagrams, int minOutputWordsLength)
        {
            var results = new List<string>();

            if (anagramCount < 1)
            {
                return results;
            }

            if (anagramCount == 1)
            {
                string targetKey = CreateKey(targetLetters);

                foreach (var anagram in allAnagrams)
                {
                    if (targetKey == anagram.Key)
                    {
                        results.Add(anagram.Words.ToString());
                    }
                }
            }

            return results;
        }

        private string CreateKey(Dictionary<char, int> lettersCount)
        {
            string key = "";

            foreach (var letter in lettersCount)
            {
                for (int i = letter.Value; i > 0; i--)
                {
                    key += letter.Key;
                }
            }

            key = _wordProcessor.SortString(key);

            return key;
        }
    }
}

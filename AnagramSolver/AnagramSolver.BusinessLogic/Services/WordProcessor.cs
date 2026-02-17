using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class WordProcessor : IWordProcessor
    {
        public Dictionary<char, int> CreateCharCount(string stringToProcess)
        {
            stringToProcess = RemoveWhitespace(stringToProcess);

            Dictionary<char, int> charDictionary = stringToProcess.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

            return charDictionary;
        }

        public string SortString(string stringToProcess)
        {
            if (string.IsNullOrEmpty(stringToProcess))
                return "";

            stringToProcess = RemoveWhitespace(stringToProcess);

            string sortedString = new string(stringToProcess.OrderBy(c => c).ToArray());

            return sortedString;
        }

        public string RemoveWhitespace(string stringToProcess)
        {
            if (string.IsNullOrEmpty(stringToProcess))
                return "";

            var result = new string(stringToProcess.Where(c => !char.IsWhiteSpace(c)).ToArray());

            return result.ToLower();
        }

    }
}
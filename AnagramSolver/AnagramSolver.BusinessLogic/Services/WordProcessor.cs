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
            var charDictionary = new Dictionary<char, int>();
            stringToProcess = RemoveWhitespace(stringToProcess);
            foreach (var character in stringToProcess.ToLower())
            {
                if (!charDictionary.ContainsKey(character))
                    charDictionary[character] = 0;
                charDictionary[character]++;
            }

            return charDictionary;
        }

        public string SortString(string stringToProcess)
        {
            if (string.IsNullOrEmpty(stringToProcess))
                return "";

            stringToProcess = RemoveWhitespace(stringToProcess);

            char[] arr = stringToProcess.ToCharArray();
            Array.Sort(arr);
            string sortedString = new string(arr);
            return sortedString;
        }

        public string RemoveWhitespace(string stringToProcess)
        {
            if (string.IsNullOrEmpty(stringToProcess))
                return "";

            var result = new string(stringToProcess.Where(c => !char.IsWhiteSpace(c)).ToArray());

            return result.ToLower();
        }
        public bool IsValidOutputLength(string key, int minOutputWordLength)
        {
            return key.Length >= minOutputWordLength;
        }

        public bool CanFitWithin(Dictionary<char, int> letters, Dictionary<char, int> targetLetters)
        {
            foreach (var letter in letters)
                if (!targetLetters.ContainsKey(letter.Key) || letter.Value > targetLetters[letter.Key])
                    return false;

            return true;
        }

    }
}
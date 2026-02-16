using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Services
{
    public class RecursiveAnagramAlgorithm : IAnagramAlgorithm
    {

        public IList<string> GetAnagrams()
        {

        }

        public List<List<string>> FindKeyCombinations(Dictionary<char, int> targetLetters, int maxWords, List<Anagram> possibleAnagrams)
        {
            var results = new List<List<string>>();
            if (maxWords > 0)
                RecursiveKeyFinder(targetLetters, maxWords, 0, possibleAnagrams, new List<string>(), results);
            else
                results = new List<List<string>> { { new List<string> { } } };

            return results;
        }

        private void RecursiveKeyFinder(Dictionary<char, int> remainingLetters, int wordsLeft, int startIndex, List<Anagram> possibleAnagrams, List<string> currentCombination, List<List<string>> keyCombinations)
        {
            if (AllLettersUsed(remainingLetters))
            {
                keyCombinations.Add(new List<string>(currentCombination));
                return;
            }

            else if (wordsLeft == 0)
                return;

            for (int i = startIndex; i < possibleAnagrams.Count; i++)
            {
                string key = possibleAnagrams[i]?.Key ?? "";
                var lettersCount = possibleAnagrams[i]?.KeyCharCount ?? new Dictionary<char, int>();
                if (CanFitWithin(lettersCount, remainingLetters))
                {
                    currentCombination.Add(key);
                    RemoveLetters(key, remainingLetters);

                    RecursiveKeyFinder(remainingLetters, wordsLeft - 1, i, possibleAnagrams, currentCombination, keyCombinations);

                    AddLetters(key, remainingLetters);
                    currentCombination.RemoveAt(currentCombination.Count - 1);
                }
            }
        }

        public List<string> CreateCombinations(List<List<string>> keyCombinations, List<Anagram> possibleAnagrams)
        {
            var results = new List<string>();

            var anagramDic = possibleAnagrams.ToDictionary(anagram => anagram.Key, anagram => anagram.Words);

            foreach (var keyCombination in keyCombinations)
                RecursiveCombinationCreator(keyCombination, anagramDic, 0, new List<string>(), results);

            return results;
        }

        private void RecursiveCombinationCreator(List<string> keyCombination, Dictionary<string, List<string>> anagramDic, int index, List<string> currentSentence, List<string> results)
        {
            if (index == keyCombination.Count)
            {
                results.Add(string.Join(" ", currentSentence));
                return;
            }

            string currentKey = keyCombination[index];
            foreach (var wordVariant in anagramDic[currentKey])
            {
                currentSentence.Add(wordVariant);
                RecursiveCombinationCreator(keyCombination, anagramDic, index + 1, currentSentence, results);
                currentSentence.RemoveAt(currentSentence.Count - 1);
            }
        }

        public bool CanFitWithin(Dictionary<char, int> letters, Dictionary<char, int> targetLetters)
        {
            foreach (var letter in letters)
                if (!targetLetters.ContainsKey(letter.Key) || letter.Value > targetLetters[letter.Key])
                    return false;

            return true;
        }

        private bool AllLettersUsed(Dictionary<char, int> remainingLetters)
        {
            foreach (var pair in remainingLetters)
            {
                if (pair.Value > 0)
                    return false;
            }

            return true;
        }
        
        private void RemoveLetters(string key, Dictionary<char, int> charCountDictionary)
        {
            foreach (var character in key)
                charCountDictionary[character]--;
        }

        private void AddLetters(string key, Dictionary<char, int> charCountDictionary)
        {
            foreach (var character in key)
                charCountDictionary[character]++;
        }

        public bool IsValidOutputLength(string key, int minOutputWordLength)
        {
            return key.Length >= minOutputWordLength;
        }
    }
}

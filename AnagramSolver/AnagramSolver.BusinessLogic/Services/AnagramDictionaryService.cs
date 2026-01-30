using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramDictionaryService : IAnagramDictionaryService
    {
        private readonly IWordProcessor _wordProcessor;

        public AnagramDictionaryService(IWordProcessor wordProcessor)
        {
            _wordProcessor = wordProcessor;
        }

        public List<Anagram> CreateAnagrams(HashSet<WordModel> wordModels)
        {
            var dictionary = new Dictionary<string, Anagram>();

            foreach (var wordModel in wordModels)
            {
                string word = wordModel.Word;

                string key = _wordProcessor.SortString(word);

                AddWordToDictionary(dictionary, key, word);
            }

            var result = dictionary.Values.ToList();

            return result;
        }

        private void AddWordToDictionary(Dictionary<string, Anagram> dictionary, string key, string word)
        {
            if (dictionary.TryGetValue(key, out var anagram))
            {
                if (!anagram.Words.Contains(word))
                    anagram.Words.Add(word);
            }
            else
            {
                dictionary[key] = CreateNewAnagramInstance(key, word);
            }
        }

        private Anagram CreateNewAnagramInstance(string key, string word)
        {
            var anagram = new Anagram
            {
                Key = key,
                KeyCharCount = _wordProcessor.CreateCharCount(key),
                Words = new List<string> { word }
            };

            return anagram;
        }
    }
}

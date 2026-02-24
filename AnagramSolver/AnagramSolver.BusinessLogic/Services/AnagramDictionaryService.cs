using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramDictionaryService : IAnagramDictionaryService
    {
        private readonly IWordProcessor _wordProcessor;
        private readonly IWordRepository _wordRepository;
        private readonly IInputValidation _inputValidation;

        public AnagramDictionaryService(IWordProcessor wordProcessor, IWordRepository wordRepository, IInputValidation inputValidation)
        {
            _wordProcessor = wordProcessor;
            _wordRepository = wordRepository;
            _inputValidation = inputValidation;
        }

        public IEnumerable<Anagram> CreateAnagrams(IEnumerable<WordModel> wordModels)
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
                CreateNewAnagramInstance(dictionary, key, word);
            }
        }

        private void CreateNewAnagramInstance(Dictionary<string, Anagram> dictionary, string key, string word)
        {
            var anagram = new Anagram
            {
                Key = key,
                KeyCharCount = _wordProcessor.CreateCharCount(key),
                Words = new List<string> { word }
            };

            dictionary[key] = anagram;

        }
    }
}

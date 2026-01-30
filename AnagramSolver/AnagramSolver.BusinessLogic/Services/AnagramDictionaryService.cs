using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

                if (!dictionary.ContainsKey(key))
                    dictionary[key] = new Anagram 
                    { 
                        Key = key, 
                        KeyCharCount = _wordProcessor.CreateCharCount(key), 
                        Words = new List<string> { word } 
                    };

                else if (!dictionary[key].Words.Contains(word))
                    dictionary[key].Words.Add(word);
            }

            var result = dictionary.Values.ToList();

            return result;
        }
    }
}

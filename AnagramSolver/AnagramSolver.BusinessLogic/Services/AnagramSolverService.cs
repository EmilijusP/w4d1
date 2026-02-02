using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramSolverService: IAnagramSolver
    {
        private readonly IWordProcessor _wordProcessor;
        private readonly IAnagramDictionaryService _anagramDictonaryService;
        private readonly IAnagramAlgorithm _anagramAlgorithm;
        private readonly IWordRepository _wordRepository;
        private readonly int _anagramCount;
        private readonly int _minOutputWordsLength;

        public AnagramSolverService(
            IWordProcessor wordProcessor,
            IAnagramDictionaryService anagramDictionaryService,
            IAnagramAlgorithm anagramAlgorithm,
            IWordRepository wordRepository,
            int anagramCount,
            int minOutputWordsLength
            )
        {
            _wordProcessor = wordProcessor;
            _anagramDictonaryService = anagramDictionaryService;
            _anagramAlgorithm = anagramAlgorithm;
            _wordRepository = wordRepository;
            _anagramCount = anagramCount;
            _minOutputWordsLength = minOutputWordsLength;
        }

        public IList<string> GetAnagrams(string userWords)
        {
            var cleanInput = _wordProcessor.RemoveWhitespace(userWords);

            var inputCharCount = _wordProcessor.CreateCharCount(cleanInput);

            var wordSet = _wordRepository.GetWords();

            var allAnagrams = _anagramDictonaryService.CreateAnagrams(wordSet);

            var filteredAnagrams = allAnagrams.Where(key => _anagramAlgorithm.IsValidOutputLength(key.Key, _minOutputWordsLength));
            
            var possibleAnagrams = filteredAnagrams.Where(key => _anagramAlgorithm.CanFitWithin(key.KeyCharCount, inputCharCount)).ToList();

            var keyCombinations = _anagramAlgorithm.FindKeyCombinations(inputCharCount, _anagramCount, possibleAnagrams);

            var anagramList = _anagramAlgorithm.CreateCombinations(keyCombinations, possibleAnagrams);

            return anagramList.Where(anagram => !string.Equals(_wordProcessor.RemoveWhitespace(anagram), cleanInput)).ToList();
        }
    }
}

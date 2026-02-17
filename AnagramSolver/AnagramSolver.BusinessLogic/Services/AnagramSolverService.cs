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
        private readonly IAnagramDictionaryService _anagramDictionaryService;
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramAlgorithmFactory _anagramAlgorithmFactory;
        private readonly IAppSettings _settings;
        private readonly IMemoryCache<IEnumerable<string>> _memoryCache;

        public AnagramSolverService(
            IWordProcessor wordProcessor,
            IAnagramDictionaryService anagramDictionaryService,
            IWordRepository wordRepository,
            IAnagramAlgorithmFactory anagramAlgorithmFactory,
            IAppSettings settings,
            IMemoryCache<IEnumerable<string>> memoryCache
            )
        {
            _wordProcessor = wordProcessor;
            _anagramDictionaryService = anagramDictionaryService;
            _wordRepository = wordRepository;
            _anagramAlgorithmFactory = anagramAlgorithmFactory;
            _settings = settings;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<string>> GetAnagramsAsync(string userWords, CancellationToken ct)
        {
            var cleanInput = _wordProcessor.RemoveWhitespace(userWords);

            IEnumerable<string> value;
            if (_memoryCache.TryGet(cleanInput, out var cached))
            {
                return cached;
            }

            var inputCharCount = _wordProcessor.CreateCharCount(cleanInput);

            var wordSet = await _wordRepository.ReadAllLinesAsync(ct);

            var allAnagrams = _anagramDictionaryService.CreateAnagrams(wordSet);

            // Pipeline? Factory pipelinui sukurt
            var filteredAnagrams = allAnagrams.Where(key => _wordProcessor.IsValidOutputLength(key.Key, _settings.MinOutputWordsLength));
            
            var possibleAnagrams = filteredAnagrams.Where(key => _wordProcessor.CanFitWithin(key.KeyCharCount, inputCharCount)).ToList();
            //

            var algorithm = _anagramAlgorithmFactory.Create(_settings.AnagramCount);

            var anagramList = algorithm.GetAnagrams(inputCharCount, _settings.AnagramCount, possibleAnagrams, _settings.MinOutputWordsLength);

            // irgi prie pipeline to pacio prijungt?
            var anagramsWithoutInput = anagramList.Where(anagram => !string.Equals(_wordProcessor.RemoveWhitespace(anagram), cleanInput)).ToList();

            _memoryCache.Add(cleanInput, anagramsWithoutInput);

            return anagramsWithoutInput;
        }
    }
}

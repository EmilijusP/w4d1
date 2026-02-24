using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramFinder : IAnagramFinder
    {
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramDictionaryService _anagramDictionaryService;
        private readonly IFilterPipeline _filterPipeline;
        private readonly IAnagramAlgorithmFactory _anagramAlgorithmFactory;
        private readonly IWordProcessor _wordProcessor;
        private readonly IAppSettings _settings;

        public AnagramFinder(
            IWordRepository wordRepository,
            IAnagramDictionaryService anagramDictionaryService,
            IFilterPipeline filterPipeline,
            IAnagramAlgorithmFactory anagramAlgorithmFactory,
            IWordProcessor wordProcessor,
            IAppSettings appSettings
            )
        {
            _wordRepository = wordRepository;
            _anagramDictionaryService = anagramDictionaryService;
            _filterPipeline = filterPipeline;
            _anagramAlgorithmFactory = anagramAlgorithmFactory;
            _wordProcessor = wordProcessor;
            _settings = appSettings;
        }

        public async Task<IEnumerable<string>> FindAnagramsAsync(Dictionary<char, int> inputCharCount, string userInput, CancellationToken ct)
        {
            var wordSet = await _wordRepository.ReadAllLinesAsync(ct);

            var allAnagrams = _anagramDictionaryService.CreateAnagrams(wordSet);

            var filteredAnagrams = _filterPipeline.Execute(allAnagrams, _settings.MinOutputWordsLength, inputCharCount).ToList();

            var algorithm = _anagramAlgorithmFactory.Create(_settings.AnagramCount);

            var anagramList = algorithm.GetAnagrams(inputCharCount, _settings.AnagramCount, filteredAnagrams, _settings.MinOutputWordsLength);

            var anagramsWithoutInput = anagramList.Where(anagram => !string.Equals(_wordProcessor.RemoveWhitespace(anagram), _wordProcessor.RemoveWhitespace(userInput)));

            return anagramsWithoutInput;

        }

    }
}

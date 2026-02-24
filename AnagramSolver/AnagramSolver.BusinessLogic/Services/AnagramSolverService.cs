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
        private readonly IMemoryCache<IEnumerable<string>> _memoryCache;
        private readonly ISearchLogRepository _searchLogRepository;
        private readonly IInputNormalization _inputNormalization;
        private readonly IAnagramFinder _anagramFinder;

        public AnagramSolverService(
            IMemoryCache<IEnumerable<string>> memoryCache,
            ISearchLogRepository searchLogRepository,
            IInputNormalization inputNormalization,
            IAnagramFinder anagramFinder
            )
        {
            _memoryCache = memoryCache;
            _searchLogRepository = searchLogRepository;
            _inputNormalization = inputNormalization;
            _anagramFinder = anagramFinder;
        }

        //testus padaryti
        public async Task<IEnumerable<string>> GetAnagramsAsync(string userInput, CancellationToken ct)
        {

            if (_memoryCache.TryGet(userInput, out var cached))
            {
                return cached;
            }

            var inputCharCount = _inputNormalization.NormalizeInput(userInput);

            var anagrams = await _anagramFinder.FindAnagramsAsync(inputCharCount, userInput, ct);

            _memoryCache.Add(userInput, anagrams);

            await _searchLogRepository.AddSearchLogAsync(userInput, anagrams.Count(), ct);

            return anagrams;
        }
    }
}

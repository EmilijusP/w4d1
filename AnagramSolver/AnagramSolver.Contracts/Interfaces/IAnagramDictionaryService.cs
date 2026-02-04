using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramDictionaryService
    {
        List<Anagram> CreateAnagrams(HashSet<WordModel> wordModels);

        Task<bool> AddWordAsync(string wordToAdd, CancellationToken ct = default);

    }

}

using AnagramSolver.Contracts.Models;
using System.Collections;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        Task<IEnumerable<WordModel>> ReadAllLinesAsync(CancellationToken ct = default);

        Task AddWordAsync(WordModel wordModel, CancellationToken ct = default);

        Task<bool> DeleteById(int id, CancellationToken ct = default);
    }
}

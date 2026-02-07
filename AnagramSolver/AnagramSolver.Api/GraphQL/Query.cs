using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Api.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<string>> GetAnagramsAsync(string word, [Service]IAnagramSolver anagramSolver, CancellationToken ct)
        {
            return await anagramSolver.GetAnagramsAsync(word, ct);
        }
    }
}

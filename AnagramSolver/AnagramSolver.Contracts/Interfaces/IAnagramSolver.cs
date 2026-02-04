namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        Task<IEnumerable<string>> GetAnagramsAsync(string userWords, CancellationToken ct = default);
    }
}

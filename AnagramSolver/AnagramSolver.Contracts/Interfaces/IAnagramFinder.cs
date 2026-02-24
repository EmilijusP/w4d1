using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramFinder
    {
        Task<IEnumerable<string>> FindAnagramsAsync(Dictionary<char, int> inputCharCount, string userInput, CancellationToken ct);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLogRepository
    {
        Task AddSearchLogAsync(string input, int resultCount, CancellationToken ct);
    }
}

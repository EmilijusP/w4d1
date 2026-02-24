using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Data
{
    public class EfWordRepository : IWordRepository, ISearchLogRepository
    {
        private readonly AnagramDbContext _context;

        public EfWordRepository(AnagramDbContext context)
        {
            _context = context;
        }

        // GetWordsByLength?    SELECT Word FROM Words WHERE Word <= @InputLength
        public async Task<IEnumerable<WordModel>> ReadAllLinesAsync(CancellationToken ct)
        {
            var query = "SELECT Lemma, Form, Word, Frequency FROM Words";

            return await _context.Database.SqlQueryRaw<WordModel>(query).ToListAsync(ct);

            //return await _context.Words.ToListAsync();
        }

        public async Task AddWordAsync(WordModel wordModel, CancellationToken ct)
        {
            await _context.Words.AddAsync(wordModel, ct);

            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteById(int id, CancellationToken ct)
        {
            var word = await _context.Words.FindAsync(id, ct);

            if (word == null)
            {
                return false;
            }

            _context.Words.Remove(word);

            await _context.SaveChangesAsync(ct);

            return true;
        }

        public async Task AddSearchLogAsync(string input, int resultCount, CancellationToken ct)
        {
            var log = new SearchLog
            {
                SearchText = input,
                ResultCount = resultCount,
                SearchedAt = DateTime.Now
            };

            await _context.SearchLogs.AddAsync(log, ct);
            await _context.SaveChangesAsync(ct);
        }
    }
}

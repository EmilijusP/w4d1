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
    public class EfWordRepository : IWordRepository
    {
        private readonly AnagramDbContext _context;

        public EfWordRepository(AnagramDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WordModel>> ReadAllLinesAsync(CancellationToken ct)
        {
            return await _context.Words.ToListAsync();
        }

        public async Task AddLineAsync(WordModel wordModel, CancellationToken ct)
        {
            await _context.AddAsync(wordModel, ct);

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

    }
}

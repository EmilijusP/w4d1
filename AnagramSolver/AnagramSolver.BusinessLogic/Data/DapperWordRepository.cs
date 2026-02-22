using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Data
{
    public class DapperWordRepository : IWordRepository, ISearchLogRepository
    {
        private readonly string _connectionString;

        public DapperWordRepository(IAppSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<IEnumerable<WordModel>> ReadAllLinesAsync(CancellationToken ct)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var query = "SELECT Id, Lemma, Form, Word, Frequency FROM Words";
            return await db.QueryAsync<WordModel>(query);
        }

        public async Task AddWordAsync(WordModel wordModel, CancellationToken ct)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var query = "INSERT INTO Words (Lemma, Form, Word, Frequency) VALUES (@Lemma, @Form, @Word, @Frequency)";
            await db.ExecuteAsync(query, wordModel);
        }

        public async Task<bool> DeleteById(int id, CancellationToken ct)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var query = "DELETE FROM Words WHERE Id = @Id";
            var affectedRows = await db.ExecuteAsync(query, new { Id = id });
            return affectedRows > 0;
        }

        public async Task AddSearchLogAsync(string input, int resultCount, CancellationToken ct)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var query = "INSERT INTO SearchLogs (SearchText, ResultCount, SearchedAt) VALUES (@Input, @ResultCount, GETDATE())";
            await db.ExecuteAsync(query, new { Input = input, ResultCount = resultCount });
        }
    }
}

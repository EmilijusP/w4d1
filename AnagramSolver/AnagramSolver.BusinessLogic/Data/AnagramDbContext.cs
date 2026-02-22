using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Data
{
    public class AnagramDbContext : DbContext
    {
        public DbSet<WordModel> Words { get; set; }

        public DbSet<SearchLog> SearchLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=.\\SQLEXPRESS;Database=AnagramSolver_DB;Trusted_Connection=True;TrustServerCertificate=True");
    }
}

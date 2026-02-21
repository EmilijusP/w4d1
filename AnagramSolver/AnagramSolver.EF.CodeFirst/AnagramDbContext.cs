using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.EF.CodeFirst
{
    public class AnagramDbContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SearchLog> SearchLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=.\\SQLEXPRESS;Database=AnagramSolver_CF;Trusted_Connection=True;TrustServerCertificate=True");
    }

}

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
    public class AnagramDbContext : DbContext
    {
        private readonly IAppSettings _settings;

        public AnagramDbContext(IAppSettings settings)
        {
            _settings = settings;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WordModel>(entity =>
            {
                entity.ToTable("Words");

                entity.HasKey(w => w.Id);

                entity.Property(w => w.Word)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(w => w.Lemma)
                    .HasMaxLength(100);

                entity.Property(w => w.Form)
                    .HasMaxLength(50);

                entity.Property(w => w.Frequency)
                    .HasDefaultValue(1);
            });

            modelBuilder.Entity<SearchLog>(entity => {
                entity.ToTable("SearchLogs");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.SearchText)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.ResultCount)
                    .HasDefaultValue(0);

                entity.Property(s => s.SearchedAt)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        public DbSet<WordModel> Words { get; set; }

        public DbSet<SearchLog> SearchLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(_settings.ConnectionString);
    }
}

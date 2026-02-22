using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Models
{
    public class SearchLog
    {
        public int Id { get; set; }

        public string SearchText { get; set; } = null!;

        public int ResultCount { get; set; }

        public DateTime? SearchedAt { get; set; } = DateTime.Now;
    }
}

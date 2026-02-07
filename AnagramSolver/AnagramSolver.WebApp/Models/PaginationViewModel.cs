using AnagramSolver.Contracts.Models;

namespace AnagramSolver.WebApp.Models
{
    public class PaginationViewModel
    {
        public IEnumerable<WordModel> Items { get; set; }

        public int CurrentPage { get; set; }

        public double TotalPages { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

    }
}

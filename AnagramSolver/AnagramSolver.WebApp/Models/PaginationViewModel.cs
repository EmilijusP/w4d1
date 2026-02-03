namespace AnagramSolver.WebApp.Models
{
    public class PaginationViewModel
    {
        public IEnumerable<string> Items { get; set; }

        public int CurrentPage { get; set; }

        public double TotalPages { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

    }
}

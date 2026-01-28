namespace AnagramSolver.Contracts.Models
{
    public class AppSettings
    {
        public int AnagramCount { get; set; }

        public int MinWordLength { get; set; }

        public string FilePath { get; set; } = string.Empty;

        public List<string>? userWords { get; set; }
    }
}
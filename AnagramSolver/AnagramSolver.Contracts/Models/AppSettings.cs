namespace AnagramSolver.Contracts.Models
{
    public class AppSettings
    {
        public int AnagramCount { get; set; }

        public int MinInputWordsLength { get; set; }

        public string FilePath { get; set; } = string.Empty;
    }
}
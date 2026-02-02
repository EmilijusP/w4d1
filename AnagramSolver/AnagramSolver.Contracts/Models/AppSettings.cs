namespace AnagramSolver.Contracts.Models
{
    public class AppSettings
    {
        public string FilePath { get; set; } = string.Empty;

        public int AnagramCount { get; set; }

        public int MinInputWordsLength { get; set; }

        public int MinOutputWordsLength { get; set; }

    }
}
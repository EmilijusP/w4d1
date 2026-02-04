using System.ComponentModel;

namespace AnagramSolver.WebApp.Models
{
    public class AnagramViewModel
    {
        public string Word { get; set; }

        public IEnumerable<string>? AnagramLines { get; set; } = new List<string>();

    }
}

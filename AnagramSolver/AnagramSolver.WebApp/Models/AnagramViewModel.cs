using System.ComponentModel;

namespace AnagramSolver.WebApp.Models
{
    public class AnagramViewModel
    {
        public IList<string>? AnagramLines { get; set; } = new List<string>();
    }
}

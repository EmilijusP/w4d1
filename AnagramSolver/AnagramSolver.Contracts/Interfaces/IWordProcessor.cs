using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordProcessor
    {
        Dictionary<char, int> CreateCharCount(string stringToProcess);

        string SortString(string unsortedString);

        string RemoveWhitespace(string stringToProcess);

        bool IsValidOutputLength(string key, int minOutputWordLength);
        
        bool CanFitWithin(Dictionary<char, int> letters, Dictionary<char, int> targetLetters);
    }
}

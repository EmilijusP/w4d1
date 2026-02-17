using AnagramSolver.BusinessLogic.Extensions;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Filters
{
    public class CharacterFitFilter : IAnagramFilter
    {
        public IEnumerable<Anagram> ApplyFilter(IEnumerable<Anagram> anagrams, int minOutputWordLength, Dictionary<char, int> inputCharCount)
        {
            var filteredAnagrams = anagrams.Where(a => a.KeyCharCount.CanFitWithin(inputCharCount));

            return filteredAnagrams;
        }

    }
}

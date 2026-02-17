using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class FilterPipeline : IFilterPipeline
    {
        private IEnumerable<IAnagramFilter> _anagramFilters;

        public FilterPipeline(IEnumerable<IAnagramFilter> anagramFilters)
        {
            _anagramFilters = anagramFilters;
        }

        public IEnumerable<Anagram> Execute(IEnumerable<Anagram> anagrams, int minOutputWordLength, Dictionary<char, int> userInputCharCount)
        {
            var currentAnagrams = anagrams;

            foreach (var filter in _anagramFilters)
            {
                currentAnagrams = filter.ApplyFilter(currentAnagrams, minOutputWordLength, userInputCharCount);
            }

            return currentAnagrams;
        }
    }
}

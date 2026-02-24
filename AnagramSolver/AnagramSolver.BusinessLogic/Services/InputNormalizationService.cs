using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class InputNormalizationService : IInputNormalization
    {
        private readonly IWordProcessor _wordProcessor;

        public InputNormalizationService(IWordProcessor wordProcessor)
        {
            _wordProcessor = wordProcessor;
        }

        public Dictionary<char, int> NormalizeInput(string input)
        {
            var cleanInput = _wordProcessor.RemoveWhitespace(input);

            var inputCharCount = _wordProcessor.CreateCharCount(cleanInput);

            return inputCharCount;
        }
    }
}

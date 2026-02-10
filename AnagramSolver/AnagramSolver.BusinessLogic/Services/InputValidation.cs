using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class InputValidation : IInputValidation
    {
        private readonly IWordRepository _wordRepository;
        private readonly IWordProcessor _wordProcessor;

        public InputValidation(IWordRepository wordRepository, IWordProcessor wordProcessor)
        {
            _wordRepository = wordRepository;
            _wordProcessor = wordProcessor;
        }

        public bool IsValidUserInput(string input, int minWordLength)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var words = input.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
            {
                return false;
            }

            foreach (string word in words)
            {
                if (word.Length < minWordLength)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> IsValidWriteToFileInputAsync(WordModel wordModel, CancellationToken ct)
        {
            var lemma = wordModel.Lemma;
            var form = wordModel.Form;
            var word = wordModel.Word;
            var frequency = wordModel.Frequency;

            if (string.IsNullOrEmpty(word))
            {
                return false;
            }

            if (word.Trim().Any(c => char.IsWhiteSpace(c)))
            {
                return false;
            }

            var allLines = await _wordRepository.ReadAllLinesAsync(ct);
            
            if (allLines.Any(model => model.Word == _wordProcessor.RemoveWhitespace(word).ToLower()))
            {
                return false;
            }

            return true;
        }
    }
}
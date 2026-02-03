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

        public InputValidation(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public bool IsValidUserInput(string input, int minWordLength)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            bool isValid = true;
            var words = input.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length == 0)
                return false;

            foreach (string word in words)
            {
                if (word.Length < minWordLength)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        public bool IsValidWriteToFileInput(WordModel wordModel)
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

            if (_wordRepository.GetWords().Any(model => model.Word == word))
            {
                return false;
            }

            return true;
        }
    }
}

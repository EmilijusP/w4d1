using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Services;

namespace AnagramSolver.Cli
{
    public class UserInterface
    {
        private readonly int _minWordLength;
        private InputValidation _inputValidation;

        public UserInterface(int minWordLength, InputValidation inputValidation)
        {
            _minWordLength = minWordLength;
            _inputValidation = inputValidation;
        }

        public string ReadInput()
        {
            do
            {
                Console.WriteLine($"Enter the word/words containing {_minWordLength} letters or more: ");
                var input = Console.ReadLine();
                if (_inputValidation.IsValidInput(input, _minWordLength))
                {
                    return input;
                }

            } while (true);
        }

        public void ShowOutput(IList<string> words)
        {
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}

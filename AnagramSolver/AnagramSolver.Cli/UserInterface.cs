using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Cli
{
    public class UserInterface
    {
        public List<string> userWords = new List<string>();
        private readonly int _minWordLength;
        private bool toBreak = true;

        public UserInterface(int minWordLength)
        {
            _minWordLength = minWordLength;
        }

        public List<string> ReadInput()
        {
            do
            {
                Console.WriteLine($"Enter the word/words containing {_minWordLength} letters or more: ");
                var input = Console.ReadLine();

                toBreak = true;
                foreach (string word in input.Split())
                {
                    if (word.Length < _minWordLength)
                        toBreak = false;

                }
                if (toBreak)
                {
                    userWords = input.Split().ToList();
                }
            } while (!toBreak);

            return userWords;
        }
    }
}

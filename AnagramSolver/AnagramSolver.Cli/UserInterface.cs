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
        public string ReadInput()
        {
            Console.WriteLine($"Enter the word/words: ");
            var input = Console.ReadLine();
            return input;
        }

        public void ShowOutput(IEnumerable<string> words)
        {
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}

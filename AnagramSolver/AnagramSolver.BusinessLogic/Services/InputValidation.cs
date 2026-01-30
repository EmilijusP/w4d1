using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class InputValidation
    {
        public bool IsValidInput(string input, int minWordLength)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            bool isValid = true;
            var words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

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
    }
}

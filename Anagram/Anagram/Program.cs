using System.Diagnostics.Tracing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace Namespace
{
    public class Program
    {
        public class Settings(int minWordLength = 1, int anagramCount = 1, string word = "anagram", string filePath = "zodynas.txt")
        {
            public int MinWordlength { get; } = minWordLength;

            public int AnagramCount { get; } = anagramCount;

            public string Word { get; } = word;

            public string FilePath { get; } = filePath;
        }

        public class UserInterface()
        {
            public int MinWordLength { get; set; }
            
            public int AnagramCount { get; set; }

            public string? Word { get; set; }

            public void ReadInput()
            {
                Console.WriteLine("Enter the minimum word length: ");
                MinWordLength = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter the anagram count: ");
                AnagramCount = Convert.ToInt32(Console.ReadLine());
                
                do
                {
                    Console.WriteLine("Enter the word containing " + MinWordLength + " letters or more: ");
                    Word = Console.ReadLine() ?? string.Empty;
                } while (Word.Count() < MinWordLength || Word == string.Empty);
                Word = Word.ToLower();
            }

            public void ShowOutput(List<string> anagramList)
            {
                foreach (string anagram in anagramList)
                    Console.WriteLine(anagram);
            }
        }

        public class FileReader(string filePath)
        {
            public List<string> ReadWords()
            {
                var words = new HashSet<string>();


                var textLines = new List<string> { };
                textLines = File.ReadAllLines(filePath).ToList();
                foreach (string textLine in textLines)
                {   
                    foreach (string word in textLine.Split())
                    {
                        words.Add(word.ToLower());
                    }
                }

                return words.ToList();
            }
        }

        public class AnagramSolver(List<string> wordList, int anagramCount)
        {
            private List<string> WordList { get; } = wordList;

            private int AnagramCount { get; } = anagramCount;

            public List<string> FindAnagrams(string word)
            {
                var dictionary = CreateDictionary(WordList);
                string sortedWord = SortWord(word);
                if (dictionary.ContainsKey(sortedWord))
                {
                    dictionary[sortedWord].Remove(word);
                    var anagramList = dictionary[sortedWord].Take(AnagramCount);
                    return anagramList.ToList();
                }
                else
                {
                    return new List<string> { "No anagrams found." };
                }
            }

            static private Dictionary<string, List<string>> CreateDictionary(List<string> wordList) {
                var dictionary = new Dictionary<string, List<string>>();
                foreach (string word in wordList)
                {
                    string sortedWord = SortWord(word);
                    if (!dictionary.ContainsKey(sortedWord))
                        dictionary[sortedWord] = new List<string>();
                    dictionary[sortedWord].Add(word);

                }

                return dictionary;
            }

            static private string SortWord(string word)
            {
                char[] wordChars = word.ToCharArray();
                Array.Sort(wordChars);
                return new string(wordChars);
            }
        }

        public static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var userInterface = new UserInterface();

            userInterface.ReadInput();

            var settings = new Settings(userInterface.MinWordLength, userInterface.AnagramCount, userInterface.Word!);

            var fileReader = new FileReader(settings.FilePath);

            var anagramSolver = new AnagramSolver(fileReader.ReadWords(), settings.AnagramCount);

            var anagrams = anagramSolver.FindAnagrams(settings.Word);

            userInterface.ShowOutput(anagrams);
        }
    }
}
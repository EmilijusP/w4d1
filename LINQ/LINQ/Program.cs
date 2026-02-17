// 1. LINQ pagrindai – žodžių filtravimas
using System.Reflection.Metadata.Ecma335;

var words = new List<string>
{
    "alus", "sula", "la", "vanduo", "programavimas",
    "katinas", "saulė", "medis", "oras", "knyga"
};

var longWords = words.Where(w => w.Length > 4);

var wordsStartWithS = words.Where(w => w.StartsWith('s'));

var orderedWords = words.OrderBy(w => w.Length);

var upperWords = words.Select(w => w.ToUpper());

var wordsGroupedByFirstLetter = words.GroupBy(w => w.FirstOrDefault());

var longestWord = words.OrderByDescending(w => w.Length).FirstOrDefault();

var wordsAtleastOneSymbol = words.All(w => w.FirstOrDefault() != default);

// 2. LINQ su skaičiais
var numbers = Enumerable.Range(1, 100).ToList();

var evenNumbers = numbers.Where(n => n % 2 == 0);

var oddNumbersSum = numbers.Where(n => n % 2 != 0).Sum();

var firstTenDivisableBy3 = numbers.Where(n => n % 3 == 0).Take(10);

var groupedNumbers = numbers.GroupBy(n => n <= 33 ? "maži" : 33 < n && n < 67 ? "vidutiniai": "dideli");

bool isPrime(int number)
{
    if (number <= 1)
    {
        return false;
    }

    for (int i = number - 1; i > 1; i--)
    {
        if (number % i == 0)
        {
            return false;
        }
    }

    return true;
}

Dictionary<int, bool> primeDictionary = numbers.ToDictionary(n => n, n => isPrime(n));

// 3. SelectMany
var sentences = new List<string> { "LINQ yra galingas", "C# yra puiki kalba", "Generics ir Delegates" };

var sentenceWords = sentences.SelectMany(s => s.Split());

var uniqueWords = sentences.SelectMany(s => s.Split()).Distinct();

Dictionary<string, int> wordRepeats = sentences.SelectMany(s => s.Split()).GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());

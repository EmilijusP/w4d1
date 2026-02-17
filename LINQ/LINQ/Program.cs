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

// 4. Join
var groups = new List<Group>
{
    new(1, "Matematika"),
    new(2, "Informatika"),
    new(3, "Fizika")
};

var students = new List<Student>
{
    new(1, "Jonas", 2),
    new(2, "Petras", 1),
    new(3, "Ona", 2),
    new(4, "Marytė", 3),
    new(5, "Antanas", 2)
};

var joined = students.Join(groups, s => s.GroupId, g => g.Id, (s, g) => new { s, g }) ;

foreach (var student in joined)
{
    Console.WriteLine($"Studentas {student.s.Name} yra grupėje {student.g.Name}");
}

var biggerGroups = groups.GroupJoin(students, g => g.Id, s => s.GroupId, (g, s) => new { g, s }).Where(x => x.s.Count() >= 3);

foreach(var group in biggerGroups)
{
    Console.WriteLine(group.g.Name);
}

// 5. Deferred execution – demonstracija
List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

var evenNums = nums.Where(n => n % 2 == 0); // sudaromas LINQ query, dar nevykdoma, saugoma nuoroda i nums ir delegatas

nums.Add(8); // pridedamas skaicius 8 prie originalaus nums List

foreach (var num in evenNums) // iteruojame LINQ query, todel query ivykdoma (dabar tikrinama ar skaiciai lyginiai)
{
    Console.WriteLine(num); // tarp skaiciu bus 8, nes pridejome 8 i nums kol evenNums turejo tik nuoroda nums ir delegata
}

nums = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

var oddNums = nums.Where(n => n % 2 != 0).ToList(); // sudaromas LINQ query ir iskart ivykdomas, nes ToList() privercia filtruoti pagal delegata ir sukurti nauja List objekta atmintyje

nums.Add(9); // pridedamas skaicius 9 prie originalaus nums List

Console.WriteLine($"oddNums has 9 (deferred execution): {oddNums.Contains(9)}"); // tikriname ar oddNums turi skaiciu 9, bus false, nes LINQ query buvo iskarto ivykdyta del ToList()

// Namespace and type declarations

record Student(int Id, string Name, int GroupId);

record Group(int Id, string Name);
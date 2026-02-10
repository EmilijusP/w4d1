
using GenericsAndDelegatesTask;
using System.Security.Cryptography;

void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

int x = 5;
int y = 10;

string name = "Emilijus";
string surname = "Pocius";

Product mayo = new Product { Label = "Mayonez", Price = 3.56 };
Product ketchup = new Product { Label = "Kethiup", Price = 2.20 };

Console.WriteLine("Before Swap:");
Console.WriteLine($"x = {x} y = {y} \nname = {name} surname = {surname} \nmayo = [label: {mayo.Label}, price: {mayo.Price}] ketchup = [label: {ketchup.Label}, price: {ketchup.Price}] \n");

Swap(ref x, ref y);
Swap(ref name, ref surname);
Swap(ref mayo, ref ketchup);

Console.WriteLine("After Swap:");
Console.WriteLine($"x = {x} y = {y} \nname = {name} surname = {surname} \nmayo = [label: {mayo.Label}, price: {mayo.Price}] ketchup = [label: {ketchup.Label}, price: {ketchup.Price}] \n");

Console.WriteLine("\n");

//-----------------------------------------

List<T> Where<T>(IEnumerable<T> source, Predicate<T> condition)
{
    List<T> result = new List<T>();
    foreach (var item in source)
    {
        if (condition(item))
        {
            result.Add(item);
        }
    }

    return result;
}

Predicate<string> isLongWord = word => word.Length > 4;
Predicate<int> isEvenNumber = num => num % 2 == 0;

List<string> stringList = new List<string> { "generic", "delegate", "cup", "coffee", "tea" };
List<int> numberList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

Console.WriteLine("Before filtering:");
Console.WriteLine($"stringList = {string.Join(", ", stringList)} \nnumbersList = {string.Join(", ", numberList)}\n");

stringList = Where(stringList, isLongWord);
numberList = Where(numberList, isEvenNumber);

Console.WriteLine("After filtering:");
Console.WriteLine($"stringList = {string.Join(", ", stringList)} \nnumbersList = {string.Join(", ", numberList)}\n");

Console.WriteLine("\n");

//-----------------------------------------

var operations = new Dictionary<string, Func<string, string>>();

operations["upper"] = str => str.ToUpper();
operations ["lower"] = str => str.ToLower();
operations ["reverse"] = str => new string(str.Reverse().ToArray());

string[] keys = ["upper", "lower", "reverse"];
string word = "Banana";

Console.WriteLine("Word before delegates:");
Console.WriteLine($"word = {word}\n");

Console.WriteLine("Word after delegates:");
foreach (var key in keys)
{
    var processedWord = operations[key](word);
    Console.WriteLine($"{key}_word = {processedWord}");
}

Console.WriteLine("\n");

//-----------------------------------------
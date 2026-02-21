
using AnagramSolver.EF.DatabaseFirst.Models;

var context = new AnagramDbContext();

var longWords = context.Words.Where(w => w.Value.Length > 4).Take(10).ToList();
var alusWords = context.Words.Where(w => w.Value == "alus").ToList();


foreach (var word in longWords)
    Console.WriteLine(word.Value);
foreach (var word in alusWords)
    Console.WriteLine(word.Value);
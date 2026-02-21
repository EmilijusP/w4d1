using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

var context = new AnagramDbContext();

/*
var daiktavardis = new Category { Name = "Daiktavardis" };

daiktavardis.Words.Add(new Word { Value = "alus" });
daiktavardis.Words.Add(new Word { Value = "sula" });
daiktavardis.Words.Add(new Word { Value = "praktika" });

context.Categories.Add(daiktavardis);

context.Words.Add(new Word { Value = "visma" });

context.SaveChanges();
*/

var words = context.Words.Include(w => w.Category).ToList();

Console.WriteLine("Zodziai:");
foreach (var word in words)
{
    Console.WriteLine($"{word.Id} {word.Value} {word.CategoryId} {word.Category?.Name ?? "No category"}");
}

var daikavardziai = context.Words.Where(w => w.Category.Name == "Daiktavardis").ToList();
Console.WriteLine("Daiktavardziai:");
foreach (var word in daikavardziai)
{
    Console.WriteLine($"{word.Id} {word.Value} {word.CategoryId} {word.Category?.Name ?? "No category"}");
}

var wordCount = context.Words.Where(w => w.CategoryId != null).GroupBy(w => w.Category.Name).Select(g => new { Name = g.Key, Count = g.Count() });
Console.WriteLine("Zodziu skaicius:");
foreach (var item in wordCount)
{
    Console.WriteLine($"{item.Name}: {item.Count}");
}

var wordsNoCategory = context.Words.Where(w => w.CategoryId == null).ToList();
Console.WriteLine("Zodziai be kategoriju:");
foreach (var word in wordsNoCategory)
{
    Console.WriteLine($"{word.Id} {word.Value}");
}
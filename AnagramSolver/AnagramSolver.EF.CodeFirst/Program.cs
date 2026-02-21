using AnagramSolver.EF.CodeFirst.Models;

var context = new AnagramDbContext();

var daiktavardis = new Category { Name = "Daiktavardis" };

daiktavardis.Words.Add(new Word { Value = "alus" });
daiktavardis.Words.Add(new Word { Value = "sula" });
daiktavardis.Words.Add(new Word { Value = "praktika" });

context.Categories.Add(daiktavardis);

context.Words.Add(new Word { Value = "visma" });

context.SaveChanges();

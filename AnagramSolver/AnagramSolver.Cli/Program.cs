using AnagramSolver.Contracts.Models;
using AnagramSolver.BusinessLogic.Data;

using System.Text.Json;
using System.Text.Json.Serialization;
using AnagramSolver.Cli;

string jsonPath = "appsettings.json";
string content = File.ReadAllText(jsonPath);
AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(content);

var repository = new WordRepository(settings.FilePath);
var words = repository.GetWords();

var userInterface = new UserInterface(settings.MinWordLength);
settings.userWords = userInterface.ReadInput();

foreach (string word in settings.userWords)
{
    Console.WriteLine(word);
}
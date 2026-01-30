using AnagramSolver.Contracts.Models;
using AnagramSolver.BusinessLogic.Data;

using System.Text.Json;
using System.Text.Json.Serialization;
using AnagramSolver.Cli;
using AnagramSolver.BusinessLogic.Services;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

string jsonPath = "appsettings.json";
string content = File.ReadAllText(jsonPath);

var settings = JsonSerializer.Deserialize<AppSettings>(content);

var wordsValidation = new InputValidation();

var wordRepository = new WordRepository(settings.FilePath);

var wordProcessor = new WordProcessor();

var anagramDictionary = new AnagramDictionaryService(wordProcessor);

var anagramAlgorithm = new AnagramAlgorithm();

var anagramSolver = new AnagramSolverLogic(wordProcessor, anagramDictionary, anagramAlgorithm, wordRepository, settings.AnagramCount);

var ui = new UserInterface(settings.MinInputWordsLength, wordsValidation);

var userInput = ui.ReadInput();
var results = anagramSolver.GetAnagrams(userInput);
ui.ShowOutput(results);
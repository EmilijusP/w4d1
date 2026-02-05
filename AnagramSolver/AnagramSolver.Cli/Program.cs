using System.Text.Json;
using System.Text.Json.Serialization;
using AnagramSolver.Cli;
using AnagramSolver.BusinessLogic.Services;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

using var client = new HttpClient();

client.BaseAddress = new Uri("https://localhost:7267");

var ui = new UserInterface();

var userInput = ui.ReadInput();

var response = await client.GetAsync($"api/anagrams/{userInput}", cts.Token);

response.EnsureSuccessStatusCode();

var json = await response.Content.ReadAsStringAsync();

var anagrams = JsonSerializer.Deserialize<List<string>>(json);

ui.ShowOutput(anagrams);
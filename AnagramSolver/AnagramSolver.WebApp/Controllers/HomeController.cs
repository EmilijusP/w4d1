using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using AnagramSolver.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("AnagramApi");
        }

        public async Task<IActionResult> Index(string? id, CancellationToken ct)
        {
            var anagramViewModel = new AnagramViewModel();

            if (!string.IsNullOrEmpty(id))
            {
                var response = await _httpClient.GetAsync($"anagrams/{id}", ct);
                
                if (response.IsSuccessStatusCode)
                {
                    var anagrams = await response.Content.ReadFromJsonAsync<List<string>>();

                    anagramViewModel.Word = id;
                    anagramViewModel.Anagrams = anagrams;
                }

                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var message = await response.Content.ReadAsStringAsync(ct);
                    anagramViewModel.ErrorMessage = message;
                }

                else
                {
                    var message = "Unexpected server error.";
                    anagramViewModel.ErrorMessage = message;
                }
            }

            return View(anagramViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

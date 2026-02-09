using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using AnagramSolver.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using AnagramSolver.Contracts.Models;
using System.Text.Json.Serialization;

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

            var lastSearch = Request.Cookies["lastSearch"];

            anagramViewModel.LastSearch = lastSearch;

            var sessionInfo = HttpContext.Session.GetString("SearchHistory");

            var searchHistory = sessionInfo == null
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(sessionInfo);

            if (!string.IsNullOrEmpty(id))
            {
                var response = await _httpClient.GetAsync($"anagrams/{id}", ct);

                Response.Cookies.Append("lastSearch", $"{id}", new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddHours(2),
                    HttpOnly = true
                });

                searchHistory.Add(id);

                HttpContext.Session.SetString("SearchHistory", JsonSerializer.Serialize(searchHistory));

                anagramViewModel.SearchHistory = searchHistory;

                if (response.Headers.TryGetValues("X-Anagram-Count", out var count))
                {
                    Response.Headers.Append("X-Anagram-Count", count.FirstOrDefault().ToString());
                }

                if (response.Headers.TryGetValues("X-Search-Duration-Ms", out var duration))
                {
                    Response.Headers.Append("X-Search-Duration-Ms", duration.FirstOrDefault().ToString());
                }

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

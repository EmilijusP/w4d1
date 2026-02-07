using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Api.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AnagramSolver.WebApp.Controllers
{
    public class WordsController : Controller
    {
        private readonly HttpClient _httpClient;

        public WordsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AnagramApi");
        }

        public async Task<IActionResult> Index(int page = 1, CancellationToken ct = default)
        {
            var pageSize = 100;

            var response = await _httpClient.GetAsync($"words/{page}/{pageSize}", ct);

            var result = await response.Content.ReadFromJsonAsync<PaginationResponse>();

            var paginationViewModel = new PaginationViewModel
            {
                Items = result.Items,

                CurrentPage = result.CurrentPage,

                TotalPages = result.TotalPages,

                HasPreviousPage = result.HasPreviousPage,

                HasNextPage = result.HasNextPage
            };

            return View(paginationViewModel);
        }

        public IActionResult New()
        {

            return View();
        }

        public async Task<IActionResult> Create(string? word, CancellationToken ct)
        {

            var creationViewModel = new CreationViewModel
            {
                Word = word
                //IsAdded = await _anagramDictionaryService.AddWordAsync(word, ct)
            };
            
            return View(creationViewModel);
        }
    }
}
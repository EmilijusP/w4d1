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

        public async Task<IActionResult> Create(string? lemma, string? form, string? word, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(word))
            {
                return View(new CreationViewModel { IsAdded = false });
            }

            var wordModel = new WordModel
            {
                Lemma = lemma ?? "-",
                Form = form ?? "-",
                Word = word,
                Frequency = 1
            };

            var response = _httpClient.PostAsJsonAsync("words", wordModel, ct);

            var creationViewModel = new CreationViewModel
            {
                Word = word,
                IsAdded = response.Result.IsSuccessStatusCode
            };
            
            return View(creationViewModel);
        }

        public async Task<IActionResult> Download(string fileName, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"words/download/{fileName}", ct);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound($"File {fileName} not found");
                }

                return StatusCode((int)response.StatusCode, $"Error downloading file {fileName}");
            }

            var stream = await response.Content.ReadAsStreamAsync(ct);

            return File(stream, "application/octet-stream", fileName);
        }
    }
}
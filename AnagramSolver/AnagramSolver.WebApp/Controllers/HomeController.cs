using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnagramSolver _anagramSolver;

        public HomeController(ILogger<HomeController> logger, IAnagramSolver anagramSolver)
        {
            _logger = logger;
            _anagramSolver = anagramSolver;
        }

        public async Task<IActionResult> Index(string? id, CancellationToken ct)
        {
            var anagramViewModel = new AnagramViewModel();

            if (!string.IsNullOrEmpty(id))
            {
                var anagrams = await _anagramSolver.GetAnagramsAsync(id, ct);
                anagramViewModel = new AnagramViewModel 
                { 
                    Word = id,
                    AnagramLines = anagrams 
                };
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

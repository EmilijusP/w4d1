using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AnagramSolver.WebApp.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramDictionaryService _anagramDictionaryService;

        public WordsController(IWordRepository wordRepository, IAnagramDictionaryService anagramDictionaryService)
        {
            _wordRepository = wordRepository;
            _anagramDictionaryService = anagramDictionaryService;
        }

        public IActionResult Index(int page = 1)
        {
            var pageSize = 100;

            var allItems = _wordRepository.GetWords().Select(wordModel => wordModel.Word).ToList();

            var items = allItems.Skip((page - 1) * pageSize).Take(pageSize);

            var totalPages = Math.Ceiling( allItems.Count() / (double)pageSize );

            var paginationViewModel = new PaginationViewModel
            {
                Items = items,

                CurrentPage = page,

                TotalPages = totalPages
            };

            return View(paginationViewModel);
        }

        public IActionResult New()
        {

            return View();
        }

        public IActionResult Create(string? word)
        {
            var creationViewModel = new CreationViewModel
            {
                Word = word,
                IsAdded = _anagramDictionaryService.AddWord(word)
            };
            
            return View(creationViewModel);
        }
    }
}
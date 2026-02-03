using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordRepository _wordRepository;

        public WordsController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IActionResult Index(int page = 1)
        {
            var pageSize = 100;

            var allItems = _wordRepository.GetWords().Select(wordModel => wordModel.Word).ToList();

            var items = allItems.Skip((page - 1) * pageSize).Take(pageSize);

            var totalPages = allItems.Count() / pageSize;

            var paginationViewModel = new PaginationViewModel
            {
                Items = items,

                CurrentPage = page,

                TotalPages = totalPages
            };

            return View(paginationViewModel);
        }
    }
}

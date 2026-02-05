using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramsController : ControllerBase
    {
        private readonly AppSettings _settings;
        private readonly IAnagramSolver _anagramSolver;
        private readonly IInputValidation _inputValidation;
        

        public AnagramsController(AppSettings settings, IAnagramSolver anagramSolver, IInputValidation inputValidation)
        {
            _settings = settings;
            _anagramSolver = anagramSolver;
            _inputValidation = inputValidation;
        }

        // GET api/<AnagramsController>/5
        [HttpGet("{word}")]
        public async Task<ActionResult<IEnumerable<string>>> Get(string word, CancellationToken ct = default)
        {
            if (!_inputValidation.IsValidUserInput(word, _settings.MinInputWordsLength))
            {
                return BadRequest("Netinkama įvestis.");
            }
            
            var results = await _anagramSolver.GetAnagramsAsync(word, ct);

            return Ok(results);
        }
    }
}

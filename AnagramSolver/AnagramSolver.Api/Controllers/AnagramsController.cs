using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnagramSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramsController : ControllerBase
    {
        private readonly IAppSettings _settings;
        private readonly IAnagramSolver _anagramSolver;
        private readonly IInputValidation _inputValidation;
        

        public AnagramsController(IAppSettings settings, IAnagramSolver anagramSolver, IInputValidation inputValidation)
        {
            _settings = settings;
            _anagramSolver = anagramSolver;
            _inputValidation = inputValidation;
        }

        [HttpGet("{word?}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync(string? word, CancellationToken ct = default)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            if (!_inputValidation.IsValidUserInput(word, _settings.MinInputWordsLength))
            {
                return BadRequest($"Bad input. Words should be made up of {_settings.MinInputWordsLength} or more letters.");
            }
            
            var results = await _anagramSolver.GetAnagramsAsync(word, ct);

            stopWatch.Stop();

            Response.Headers.Append("X-Anagram-Count", results.Count().ToString());
            Response.Headers.Append("X-Search-Duration-Ms", stopWatch.ElapsedMilliseconds.ToString());

            return Ok(results);
        }
    }
}

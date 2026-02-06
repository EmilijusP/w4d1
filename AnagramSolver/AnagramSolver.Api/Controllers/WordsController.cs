using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace AnagramSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordRepository _wordRepository;

        public WordsController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordModel>>> GetWordsAsync(CancellationToken ct = default)
        {
            var words = await _wordRepository.ReadAllLinesAsync(ct);

            return Ok(words);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordModel>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var words = await _wordRepository.ReadAllLinesAsync(ct);

            var word = words.FirstOrDefault(x => x.Id == id);

            if(word == null)
            {
                return NotFound($"Word with id {id} does not exist");
            }
            
            return Ok(word);
        }


    }
}

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
        private readonly IInputValidation _inputValidation;

        public WordsController(IWordRepository wordRepository, IInputValidation inputValidation)
        {
            _wordRepository = wordRepository;
            _inputValidation = inputValidation;
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

        [HttpPost]
        public async Task<ActionResult> AddWord([FromBody] WordModel wordModel, CancellationToken ct = default)
        {
            if (!await _inputValidation.IsValidWriteToFileInputAsync(wordModel, ct))
            {
                return BadRequest($"The input \"{wordModel.Word}\" is not valid.");
            }

            var words = await _wordRepository.ReadAllLinesAsync(ct);
            int newId = words.Count() + 1;
            wordModel.Id = newId;

            await _wordRepository.WriteToFileAsync(wordModel, ct);

            return CreatedAtAction("GetById", new { id = wordModel.Id}, wordModel);
        }

    }
}

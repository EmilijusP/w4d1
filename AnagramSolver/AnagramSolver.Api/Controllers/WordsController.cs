using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Collections;

namespace AnagramSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordRepository _wordRepository;
        private readonly IInputValidation _inputValidation;
        private readonly AppSettings _settings;

        public WordsController(IWordRepository wordRepository, IInputValidation inputValidation, AppSettings settings)
        {
            _wordRepository = wordRepository;
            _inputValidation = inputValidation;
            _settings = settings;
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

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWordAsync(int id, CancellationToken ct = default)
        {
            
            bool deleted = await _wordRepository.DeleteById(id, ct);

            if (!deleted)
            {
                return NotFound($"Word with id {id} not found.");
            }

            return NoContent();
        }

        [HttpGet("download")]
        public IActionResult DownloadFile()
        {
            string filePath = _settings.FilePath;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File does not exist.");
            }

            return PhysicalFile(System.IO.Path.GetFullPath(filePath), "text/plain", "zodynas.txt");
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<PaginationResponse>> GetCurrentPageWordsAsync(int page, int pageSize, CancellationToken ct = default)
        {
            var words = await _wordRepository.ReadAllLinesAsync(ct);

            var totalPages = (int) Math.Ceiling(words.Count() / (double)pageSize);

            var items = words.Skip((page - 1) * pageSize).Take(pageSize);

            bool hasPreviousPage = page > 1;

            bool hasNextPage = page < totalPages;

            var response = new PaginationResponse
            {
                Items = items,
                CurrentPage = page,
                TotalPages = totalPages,
                HasPreviousPage = hasPreviousPage,
                HasNextPage = hasNextPage
            };

            return Ok(response);
        }
    }
}

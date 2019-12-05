using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Services;
using src.Storage;

namespace src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubtitlesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IStorage _storage;

        public SubtitlesController(
            IMoviesService moviesService,
            IStorage storage)
        {
            _moviesService = moviesService;
            _storage = storage;
        }

        /// <summary>
        /// Upload subtitles to storage
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file.Length == 0 || string.IsNullOrEmpty(file.FileName))
                return BadRequest("File is empty");

            string fileContent;

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                fileContent = await stream.ReadToEndAsync();
            }
            
            if (string.IsNullOrEmpty(fileContent))
                return BadRequest("Empty text");

            var movie = _moviesService.CreateMovie(IdGenerator.GetNewId(), fileContent);
            _storage.Put(movie.Id, movie);
            
            return Ok(movie);
        }
        
        /// <summary>
        /// Gets movie by id
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(long id)
        {
            var movie = _storage.Get(id);
            return movie == null ? (IActionResult) NotFound() : Ok(movie);
        }
        
        /// <summary>
        /// Gets words entiries from movie by id
        /// </summary>
        [HttpGet("{id}/words")]
        [Produces("application/json")]
        public async Task<IActionResult> GetWords(long id)
        {
            var movie = _storage.Get(id);
            if (movie == null) return NotFound();

            return Ok(movie.Words);
        }
    }
}
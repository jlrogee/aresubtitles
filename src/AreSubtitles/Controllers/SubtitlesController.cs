using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Services.Parsers;

namespace src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubtitlesController : ControllerBase
    {
        private readonly ISrtParser _srtParser;

        public SubtitlesController(ISrtParser srtParser)
        {
            _srtParser = srtParser;
        }

        /// <summary>
        /// Upload subtitles to storage
        /// </summary>
        [HttpPost]
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
            
            var subs = _srtParser.Parse(fileContent);

            var subtitleItems = subs as SubtitleItem[] ?? subs.ToArray();
            if (!subtitleItems.Any())
                return Ok("No subs found");
            
            return Ok(subtitleItems);
        }
        
    }
}
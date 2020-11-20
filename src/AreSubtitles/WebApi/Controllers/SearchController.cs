using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Sourcing.Http.OpenSubtitles;
using Domain.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using ICSharpCode.SharpZipLib.Zip;
using Sourcing;

namespace src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly IOpenSubtitleService _openSubtitleService;
        private readonly IMoviesService _moviesService;
        
        public SearchController(
            IMoviesService moviesService,
            IOpenSubtitleService openSubtitleService)
        {
            _moviesService = moviesService;
            _openSubtitleService = openSubtitleService;
        }
        
        [HttpGet("{imdbId}")]
        public async Task<IActionResult> ByImdbId(string imdbId)
        {
            var films = await _openSubtitleService.GetByImdbIdRaw(imdbId);

            var topContent = 
                films.OrderByDescending(x => x.Score).FirstOrDefault();
            
            if (topContent == null)
                return Ok(HttpStatusCode.NotFound);

            await using (var stream = 
                new MemoryStream(new WebClient().DownloadData(topContent.ZipDownloadLink)))
            await using (var zipInputStream = new ZipInputStream(stream))
            {
                var entry = zipInputStream.GetNextEntry();
                if (entry.IsFile && entry.Name.Contains(SubFormat.Srt.ToString().ToLower()))
                {
                    var outputBuffer = new byte[zipInputStream.Length];  
                    zipInputStream.Read(outputBuffer, (int)entry.Offset, outputBuffer.Length);
                    var text = Encoding.UTF8.GetString(outputBuffer);
                    
                    if (string.IsNullOrEmpty(text))
                        return BadRequest("Empty text");

                    var movie = await _moviesService.CreateMovie(text);
                    return Ok(movie);
                }
            }
            
            return Ok(films);
        }

        private List<SubtitlesByImdbIdDtoShort> FilterByLanguage(
            IEnumerable<SubtitlesByImdbIdDtoShort> films, LanguageIso639 language)
            => films.Where(x => x.Language == language).ToList();
    }
}
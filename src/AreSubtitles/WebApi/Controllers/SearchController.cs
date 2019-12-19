using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Providers;
using Application.Services;
using Application.Sourcing.Http.OpenSubtitles;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Zip;

namespace src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly IImdbIdProvider _imdbIdProvider;
        private readonly IMoviesService _moviesService;
        
        public SearchController(
            IImdbIdProvider imdbIdProvider,
            IMoviesService moviesService)
        {
            _imdbIdProvider = imdbIdProvider;
            _moviesService = moviesService;
        }
        
        [HttpGet("{imdbId}")]
        public async Task<IActionResult> ByImdbId(string imdbId)
        {
            var raw = await _imdbIdProvider.GetByImdbIdRaw(imdbId);
            var films = JsonConvert.DeserializeObject<List<SubtitlesByImdbIdDtoSimple>>(raw);

            films = FilterByLanguage(films, LanguageIso639.En);
            var topContent = films.OrderByDescending(x => x.Score).FirstOrDefault();
            
            if (topContent == null)
                return Ok(HttpStatusCode.NotFound);

            using (var stream = new MemoryStream(
                new WebClient().DownloadData(topContent.ZipDownloadLink)))
            using (var zipInputStream = new ZipInputStream(stream))
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
            
            return Ok(raw);
        }

        private List<SubtitlesByImdbIdDtoSimple> FilterByLanguage(List<SubtitlesByImdbIdDtoSimple> films, LanguageIso639 language)
            => films.Where(x => x.Language == language).ToList();
    }
}
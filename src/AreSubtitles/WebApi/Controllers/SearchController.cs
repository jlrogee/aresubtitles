using System.Threading.Tasks;
using Application.Providers;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly IImdbIdProvider _imdbIdProvider;
        
        public SearchController(IImdbIdProvider imdbIdProvider)
        {
            _imdbIdProvider = imdbIdProvider;
        }
        
        [HttpGet("{imdbId}")]
        public async Task<IActionResult> ByImdbId(string imdbId)
        {
            var raw = await _imdbIdProvider.GetByImdbIdRaw(imdbId);
            return Ok(raw);
        }
    }
}
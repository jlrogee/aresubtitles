using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Sourcing.Http.OpenSubtitles;
using Domain.Entities;
using Newtonsoft.Json;

namespace Sourcing
{
    public class OpenSubtitlesService : IOpenSubtitleService
    {
        private readonly string _basePath = "https://rest.opensubtitles.org/";

        private readonly OpenSubtitleClient _client;

        public OpenSubtitlesService(OpenSubtitleClient client)
        {
            _client = client;
        }

        public async Task<List<SubtitlesByImdbIdDtoShort>> GetByImdbIdRaw(string imdbId,
            Language language = Language.Eng)
        {
            var url = $"{_basePath}search/imdbid-{imdbId}/sublanguageid-{language}";
            var content = await _client.Get(url);
            var films = JsonConvert.DeserializeObject<List<SubtitlesByImdbIdDtoShort>>(content);
            return films;
        }
    }
}
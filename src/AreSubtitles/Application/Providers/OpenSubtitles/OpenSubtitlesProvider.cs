using System.Threading.Tasks;
using Application.Sourcing.Http.OpenSubtitles;
using Domain.Entities;

namespace Application.Providers.OpenSubtitles
{
    public class OpenSubtitlesProvider : IImdbIdProvider
    {
        private readonly GetSubtitlesByImdbId _getSubtitlesByImdbId;

        public OpenSubtitlesProvider(GetSubtitlesByImdbId getSubtitlesByImdbId)
        {
            _getSubtitlesByImdbId = getSubtitlesByImdbId;
        }

        public async Task<string> GetByImdbIdRaw(string imdbId, Language language = Language.Eng)
        {
            return await _getSubtitlesByImdbId.GetContentByImdbId(imdbId, Language.Eng);
        }
    }
}
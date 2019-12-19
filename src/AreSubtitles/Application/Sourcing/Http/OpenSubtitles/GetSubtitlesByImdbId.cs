using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Http.Abstract;

namespace Application.Sourcing.Http.OpenSubtitles
{
    public class GetSubtitlesByImdbId : HttpRequestBase
    {
        private readonly string requestUriTemplate = "https://rest.opensubtitles.org/search/imdbid-{0}/sublanguageid-{1}";
        
        public GetSubtitlesByImdbId(IHttpClientFactory  clientFactory) 
            : base(clientFactory)
        {
        }

        public async Task<string> GetContentByImdbId(string imdbId, Language language = Language.Eng)
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "AreSubtitlesUA");
            var requestUri = string.Format(requestUriTemplate, imdbId, language);
            return await Client.GetStringAsync(requestUri);
        }
    }
}
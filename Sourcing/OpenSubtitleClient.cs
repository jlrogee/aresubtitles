using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Http.Abstract;

namespace Sourcing
{
    public class OpenSubtitleClient : HttpRequestBase
    {
        public OpenSubtitleClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }
        
        public async Task<string> Get(string url)
        {
            AddHeaders();
            return await Client.GetStringAsync(url);
        }
        
        private void AddHeaders()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "AreSubtitlesUA");
        }
    }
}
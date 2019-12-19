
using System.Net.Http;

namespace Infrastructure.Http.Abstract
{
    public interface IHttpRequest
    {
    }
    
    public abstract class HttpRequestBase : IHttpRequest
    {
        protected HttpClient Client { get; }

        protected HttpRequestBase(IHttpClientFactory clientFactory)
        {
            Client = clientFactory.CreateClient(GetType().Name);
        }
    }

}
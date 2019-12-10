using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Providers
{
    public interface IImdbIdProvider
    {
        Task<string> GetByImdbIdRaw(string imdbId, Language language = Language.Eng);
    }
}
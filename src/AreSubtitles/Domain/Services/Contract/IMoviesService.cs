using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services.Contract
{
    public interface IMoviesService
    {
        Task<FilmDocument> CreateMovie(string rawContent);

        Task<FilmDocument> GetMovie(string id);
    }
}
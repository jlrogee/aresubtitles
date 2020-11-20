using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Persistence.Contract
{
    public interface IFilmRepository : IRepository<FilmDocument>
    {
        
        // TODO remove stupid method, bad logic
        Task<FilmDocument> GetIdByHashcode(int getHashCode);
    }
}
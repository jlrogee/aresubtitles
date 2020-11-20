using System.Threading.Tasks;
using Domain.Entities;
using Domain.Persistence.Contract;
using Infrastructure.Db.Repository;

namespace Application.Persistence.Implementations
{
    public class FilmRepository : Repository<FilmDocument>, IFilmRepository
    {
        public FilmRepository(IDbContext context) : base(context)
        {
        }

        public async Task<FilmDocument> GetIdByHashcode(int getHashCode)
        {
            return await GetOne(F.Eq(x => x.Hashcode, getHashCode));
        }
    }
    
    // TODO change on auto registration
}
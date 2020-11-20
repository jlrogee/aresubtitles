using Domain.Entities;
using Domain.Persistence.Contract;

namespace Infrastructure.Db.Repository
{
    public abstract class Repository<TDocument> : WriteAndGetDbOperation<TDocument>, IRepository<TDocument> where TDocument : class, IDocument 
    {
        protected Repository(IDbContext context) : base(context)
        {
        }
    }
}
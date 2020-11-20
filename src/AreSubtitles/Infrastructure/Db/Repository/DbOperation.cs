using Domain.Entities;
using Domain.Persistence.Contract;
using MongoDB.Driver;

namespace Infrastructure.Db.Repository
{
    public abstract class DbOperation<TDocument> where TDocument : IDocument
    {
        private readonly IDbContext _context;

        protected DbOperation(IDbContext context)
        {
            _context = context;
        }

        private protected IMongoCollection<TDocument> Collection => _context.GetCollection<TDocument>();
        protected FilterDefinitionBuilder<TDocument> F => Builders<TDocument>.Filter;
    }
}
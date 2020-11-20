using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Persistence.Contract;
using MongoDB.Driver;

namespace Infrastructure.Db.Repository
{
    public abstract partial class WriteAndGetDbOperation<TDocument> : DbOperation<TDocument>  where TDocument : IDocument
    {
        protected WriteAndGetDbOperation(IDbContext context) : base(context)
        {
        }
        
        public Task<TDocument> ById(string id, 
            SortDefinition<TDocument> sorting = null, CancellationToken cancellationToken = default)
        {
            var filter = F.Eq(x => x.Id, id);
            
            return Collection.Find(filter)
                .Sort(sorting)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        
        
        public Task<TDocument> ByIds(IEnumerable<string> ids, 
            SortDefinition<TDocument> sorting = null, CancellationToken cancellationToken = default)
        {
            var filter = F.In(x => x.Id, ids);
            
            return Collection.Find(filter)
                .Sort(sorting)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        public Task<TDocument> GetOne(FilterDefinition<TDocument> filter, 
            SortDefinition<TDocument> sorting = null,
            CancellationToken cancellationToken = default)
        {
            return Collection.Find(filter)
                .Sort(sorting)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        public async IAsyncEnumerable<TDocument> Get(FilterDefinition<TDocument> filter,
            int? limit = null,
            int? batchSize = null,
            SortDefinition<TDocument> sorting = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var options = new FindOptions<TDocument>
            {
                Sort = sorting,
                Limit = limit,
                BatchSize = batchSize
            };
            var cursor = await Collection.FindAsync(filter, options, cancellationToken);
            foreach (var document in cursor.ToEnumerable())
            {
                yield return document;
            }
        }
    }
}
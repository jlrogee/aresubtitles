using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MongoDB.Driver;

namespace Domain.Persistence.Contract
{
    public interface IRepository<TDocument> where TDocument : class, IDocument
    {
        Task<TDocument> ById(string id, 
            SortDefinition<TDocument> sorting = null, CancellationToken cancellationToken = default);

        Task<TDocument> ByIds(IEnumerable<string> ids, 
            SortDefinition<TDocument> sorting = null, CancellationToken cancellationToken = default);

        IAsyncEnumerable<TDocument> Get(FilterDefinition<TDocument> filter,
            int? limit = null,
            int? batchSize = null,
            SortDefinition<TDocument> sorting = null, CancellationToken cancellationToken = default);

        Task<TDocument> Insert(TDocument doc, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDocument>> InsertMany(List<TDocument> docs, CancellationToken cancellationToken = default);
        Task<TDocument> Upsert(TDocument doc, CancellationToken cancellationToken = default);
        Task<TDocument> Upsert(FilterDefinition<TDocument> filter, TDocument doc, CancellationToken cancellationToken = default);

        Task<TDocument> UpdateOne(FilterDefinition<TDocument> filter, 
            UpdateDefinition<TDocument> update,
            bool isUpsert = false, CancellationToken cancellationToken = default, bool setUpdateInfo = true,
            SortDefinition<TDocument> sorting = null);

        Task UpdateMany(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken = default);

        Task<DeleteResult> RemoveOne(FilterDefinition<TDocument> filter,
            CancellationToken cancellationToken = default);

        Task<DeleteResult> RemoveMany(FilterDefinition<TDocument> filter,
            CancellationToken cancellationToken = default);

        Task<TDocument> Replace(TDocument oldDoc, TDocument newDoc,
            CancellationToken cancellationToken = default);
    }
}
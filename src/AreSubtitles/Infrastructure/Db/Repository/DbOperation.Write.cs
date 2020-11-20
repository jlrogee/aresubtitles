using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Db.Repository
{
    public abstract partial class WriteAndGetDbOperation<TDocument> : DbOperation<TDocument>  where TDocument : IDocument
    {
        public virtual async Task<TDocument> Insert(TDocument doc, CancellationToken cancellationToken = default)
        {
            await Collection.InsertOneAsync(doc, cancellationToken: cancellationToken);
            return doc;
        }
        
        public virtual async Task<IEnumerable<TDocument>> InsertMany(List<TDocument> docs, CancellationToken cancellationToken = default)
        {
            await Collection.InsertManyAsync(docs, cancellationToken: cancellationToken);
            return docs;
        }
        
        public virtual async Task<TDocument> Upsert(TDocument doc, CancellationToken cancellationToken = default)
        {
            var filter =  F.Eq(x => x.Id, doc.Id);
            
            await Collection.ReplaceOneAsync(filter, doc, new ReplaceOptions
            {
                IsUpsert = true
            }, cancellationToken);
            return doc;
        }
        
        public virtual async Task<TDocument> Upsert(FilterDefinition<TDocument> filter, TDocument doc, CancellationToken cancellationToken = default)
        {
            await Collection.ReplaceOneAsync(filter, doc, new ReplaceOptions
            {
                IsUpsert = true
            }, cancellationToken);
            return doc;
        }

        public virtual async Task<TDocument> UpdateOne(FilterDefinition<TDocument> filter, 
            UpdateDefinition<TDocument> update,
            bool isUpsert = false, CancellationToken cancellationToken = default, bool setUpdateInfo = true,
            SortDefinition<TDocument> sorting = null)
        {
            return await Collection.FindOneAndUpdateAsync(filter, update,
                new FindOneAndUpdateOptions<TDocument, TDocument>
                {
                    ReturnDocument = ReturnDocument.After,
                    IsUpsert = isUpsert,
                    Sort = sorting
                }, cancellationToken);
        }

        public virtual async Task UpdateMany(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken = default)
        {
            await Collection.UpdateManyAsync(filter, update, cancellationToken: cancellationToken);
        }

        public virtual async Task<DeleteResult> RemoveOne(FilterDefinition<TDocument> filter,
            CancellationToken cancellationToken = default)
        {
            return await Collection.DeleteOneAsync(filter, cancellationToken);
        }

        public virtual async Task<DeleteResult> RemoveMany(FilterDefinition<TDocument> filter,
            CancellationToken cancellationToken = default)
        {
            return await Collection.DeleteManyAsync(filter, cancellationToken);
        }

        public virtual async Task<TDocument> Replace(TDocument oldDoc, TDocument newDoc,
            CancellationToken cancellationToken = default)
        {
            var filter = F.Eq(x => x.Id, newDoc.Id);

            var replaceResult = await Collection.ReplaceOneAsync(filter, newDoc, cancellationToken: cancellationToken);
            return newDoc;
        }
    }
}
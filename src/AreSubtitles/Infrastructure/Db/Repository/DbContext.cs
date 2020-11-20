using System;
using System.Linq;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Db.Repository
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _db;
        public DbContext()
        {
            var connectionString = "mongodb://airts:j538onem@80.249.146.52";
            _db = new MongoClient(connectionString).GetDatabase("films");
        }
        
        public IMongoCollection<TDocument> GetCollection<TDocument>(string dbName = null)
            where TDocument : IDocument
        {
            return _db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }
        
        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute) documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute), true)
                .FirstOrDefault())?.CollectionName;
        }
    }
    
    public interface IDbContext
    {
        IMongoCollection<TDocument> GetCollection<TDocument>(string dbName = null) where TDocument : IDocument;
    }
}
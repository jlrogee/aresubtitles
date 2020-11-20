using System;
using System.Linq;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Db.Repository
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoClient _client;
        public DbContext()
        {
            var connectionString = "mongodb://airts:j538onem@80.249.146.52";
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase("films");
        }
        
        public IMongoCollection<TDocument> GetCollection<TDocument>(string dbName = null, string collection = null)
            where TDocument : IDocument
        {
            var (db, collectionName) = GetCollectionNameOrDefault(typeof(TDocument), collection);
            if (!string.IsNullOrEmpty(dbName??db))
            {
                return _client.GetDatabase(dbName??db).GetCollection<TDocument>(collectionName);
            }
            
            return _db.GetCollection<TDocument>(collectionName);
        }
        
        private (string, string) GetCollectionNameOrDefault(Type documentType, string collection)
        {
            var attribute = (CollectionAttribute) documentType.GetCustomAttributes(
                    typeof(CollectionAttribute), true)
                .FirstOrDefault();

            if (attribute == null && collection == null)
                throw new SubtitleCollectionNotDefinedException("Collection to use not defined");

            return (attribute?.DbName, attribute?.CollectionName ?? collection);
        }
    }

    internal class SubtitleCollectionNotDefinedException : Exception
    {
        public SubtitleCollectionNotDefinedException(string message)
            :base(message)
        {
        }
    }

    public interface IDbContext
    {
        IMongoCollection<TDocument> GetCollection<TDocument>(string dbName = null, string collection = null)
            where TDocument : IDocument;
    }
}
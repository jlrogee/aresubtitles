using System.Security.Cryptography;
using MongoDB.Bson;

namespace Domain.Services.Contract
{
    public static class IdGenerator
    {
        public static string NewId => ObjectId.GenerateNewId().ToString();
        public static string GetNewId() => ObjectId.GenerateNewId().ToString();

        public static string GetSecureNewId()
        {
            using var rng = RandomNumberGenerator.Create();
            var id = ObjectId.GenerateNewId().ToByteArray();
            // https://docs.mongodb.com/v3.6/reference/method/ObjectId/
            rng.GetBytes(id, 4, 5);
            // возвращаем как строку
            return new ObjectId(id).ToString();
        }
        
        public static string Empty => ObjectId.Empty.ToString();
    }
}
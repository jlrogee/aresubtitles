using System;

namespace Domain.Entities
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CollectionAttribute : Attribute
    {
        public string CollectionName { get; }
        public string DbName { get; set; }

        public CollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
        
        public CollectionAttribute(string dbName, string collectionName)
        {
            DbName = dbName;
            CollectionName = collectionName;
        }
    }
}
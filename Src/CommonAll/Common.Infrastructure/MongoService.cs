namespace Common.Infrastructure
{
    using Common.Core;
    using MongoDB.Driver;
    using System;

    public class MongoService : IMongoService
    {
        private readonly MongoUrlBuilder mongoUrlBuilder;

        public MongoService(string connectionUrl)
        {
            this.mongoUrlBuilder = new MongoUrlBuilder(connectionUrl);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            string collectionName = $"{typeof(T).Name}s";
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(mongoUrlBuilder.DatabaseName);
            IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(collectionName);

            return collection;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName, string databaseName)
        {
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(databaseName);
            IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(collectionName);

            return collection;
        }

        public IMongoDatabase GetDatabase(string databaseName)
        {
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(databaseName);
            
            return mongoDatabase;
        }
    }
}

namespace Inventory.Repository
{
    using Common.Core;
    using Inventory.Domain;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class StoreRepository : IStoreRepository
    {
        private readonly IMongoDbService mongoDbService;

        public StoreRepository(IMongoDbService mongoDbService)
        {
            this.mongoDbService = mongoDbService;
        }

        public async Task<Store> GetById(string id)
        {
            IMongoCollection<Store> storeCollection = mongoDbService.GetCollection<Store>();

           IAsyncCursor<Store> storeCursor = await  storeCollection.FindAsync(item => item.Id.Equals(id));

            return await storeCursor.FirstOrDefaultAsync();
        }

        public async Task<Store> Save(Store entity)
        {
            IMongoCollection<Store> storeCollection = mongoDbService.GetCollection<Store>();

            await storeCollection.InsertOneAsync(entity);

            return entity;
        }
    }
}
